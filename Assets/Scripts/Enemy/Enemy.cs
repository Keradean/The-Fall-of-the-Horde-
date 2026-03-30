using Castle;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace Enemy
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private EnemyStats enemyStats;

		[FormerlySerializedAs("Health")] [HideInInspector] public float health;
		[HideInInspector] public float speedMod = 1f;

		[FormerlySerializedAs("_path")] public Path.Path path;
		private int _currentWayPoint;
		private bool _reachedTheEnd;
	
		private float _attackCounter;
		private CastleHealth _castleHealth;
		private EnemyHealth _enemyHealth;

		public int chooseAPointOfAttack;


		private float _burnTimer;
		private float _burnDamage;
		private IObjectPool<Enemy> _pool;

		public bool isFlying;
		public float flyHeight;

		////////////////////////////////////////////////////////////////////////////////////////////////
		public void SetPool(IObjectPool<Enemy> pool)
		{
			_pool = pool; 
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void ReturnToPool()
		{
			_pool?.Release(this);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void Awake()
		{
			_enemyHealth = GetComponent<EnemyHealth>(); // cachen
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		void Start()
		{
			if(path == null)
			{
				path = FindFirstObjectByType<Path.Path>();
			}

			if(_castleHealth == null)
			{
				_castleHealth = FindFirstObjectByType<CastleHealth>();
			}
			if (isFlying)
			{
				transform.position += Vector3.up * flyHeight;
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////////////
		void Update()
		{
			MoveAndAttack();

			if (!(_burnTimer > 0)) return;
			_burnTimer -= Time.deltaTime;
			_enemyHealth.TakeDamage(_burnDamage * Time.deltaTime);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void MoveAndAttack()
		{
			var moveStep = enemyStats.moveSpeed * Time.deltaTime * speedMod;
			if (!_reachedTheEnd)
			{
				// flying...
				if(!isFlying)
				{
					transform.position = Vector3.MoveTowards(transform.position, path.wayPoints[_currentWayPoint].position, moveStep );
					transform.LookAt(path.wayPoints[_currentWayPoint].position);
					if (!(Vector3.Distance(transform.position, path.wayPoints[_currentWayPoint].position) < .01f)) return;
					_currentWayPoint++;
					if (_currentWayPoint < path.wayPoints.Length) return;
					_reachedTheEnd = true;
					chooseAPointOfAttack = Random.Range(0, _castleHealth.pointsOfAttack.Length);
				}
				else
				{
					transform.position = Vector3.MoveTowards(transform.position, path.wayPoints[_currentWayPoint].position + (Vector3.up * flyHeight), moveStep );
					transform.LookAt(path.wayPoints[_currentWayPoint].position);
					if (!(Vector3.Distance(transform.position,
						    path.wayPoints[_currentWayPoint].position + (Vector3.up * flyHeight)) < .01f)) return;
					_currentWayPoint++;
					if (_currentWayPoint < path.wayPoints.Length) return;
					_reachedTheEnd = true;
					chooseAPointOfAttack = Random.Range(0, _castleHealth.pointsOfAttack.Length);
				}
			}
			else
			{
				transform.position = !isFlying ? Vector3.MoveTowards(transform.position, _castleHealth.pointsOfAttack[chooseAPointOfAttack].position, moveStep) 
					: Vector3.MoveTowards(transform.position, _castleHealth.pointsOfAttack[chooseAPointOfAttack].position + (Vector3.up * flyHeight), moveStep);
			
				_attackCounter -= Time.deltaTime;
				if (!(_attackCounter <= 0)) return;
				_attackCounter = enemyStats.timeBetweenAttacks;
				_castleHealth.TakeDamage(enemyStats.damagePerAttack);
			}
		
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		// Reset the Enemy so he can Spawn with full life ...
		public void ResetEnemy()
		{
			// Reset Health back to MaxHealth
			health = enemyStats.maxHealth;
			// ToDo Reset other things that has to be reset!!!
			_currentWayPoint = 0;
			_reachedTheEnd = false;
			_attackCounter = 0f;
			speedMod = 1f; // back to normal speed
			_burnDamage = 0f;
			_burnTimer = 0f;
		
			// flug Gegner werden auf Ihrer Höhe resetet
			if (isFlying)
			{
				transform.position +=  Vector3.up * flyHeight ;
			}
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void Setup(CastleHealth newCastle, Path.Path newPath)
		{
			path = newPath;
			_castleHealth = newCastle;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void SetOnFire(float damaged, float duration)
		{
			_burnDamage = damaged;
			_burnTimer = duration; 
		}
	}
}
