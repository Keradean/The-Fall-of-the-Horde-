using System.Collections;
using Castle;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace Enemy
{
	public class Enemy : MonoBehaviour
	{
		private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
		private static readonly int IsDeath = Animator.StringToHash("isDeath");
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
		private Animator _animator;
		private Collider _collider;
		private bool _isDead;
		private static readonly int IsDancing = Animator.StringToHash("isDancing");
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void SetPool(IObjectPool<Enemy> pool)
		{
			_pool = pool; 
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void ReturnToPool()
		{
			_pool?.Release(this);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void Awake()
		{
			_enemyHealth = GetComponent<EnemyHealth>(); // cachen
			_animator = GetComponent<Animator>(); // hole dir die Componente
			_collider = GetComponent<Collider>();//cachen
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void Start()
		{
			if(path == null)
			{
				path = FindFirstObjectByType<Path.Path>();
			}

			if(_castleHealth == null)
			{
				_castleHealth = FindFirstObjectByType<CastleHealth>();
			}
			if(isFlying)
			{
				transform.position += Vector3.up * flyHeight;
				_currentWayPoint = path.wayPoints.Length - 1; 
			}
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void Update()
		{
			if (_isDead) return;
			MoveAndAttack();

			if (!(_burnTimer > 0)) return;
			_burnTimer -= Time.deltaTime;
			_enemyHealth.TakeDamage(_burnDamage * Time.deltaTime);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void MoveAndAttack()
		{
			if(path == null || _castleHealth == null) return;
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
					transform.LookAt(path.wayPoints[_currentWayPoint].position + Vector3.up * flyHeight);
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
				var targetPos = !isFlying 
					? _castleHealth.pointsOfAttack[chooseAPointOfAttack].position 
					: _castleHealth.pointsOfAttack[chooseAPointOfAttack].position + Vector3.up * flyHeight;
				
				transform.position = Vector3.MoveTowards(transform.position, targetPos, moveStep);

				var distanceToCastle = Vector3.Distance(transform.position, targetPos);
				if(distanceToCastle > 0.1f) return;
				_attackCounter -= Time.deltaTime;
				if (!(_attackCounter <= 0)) return;
				_attackCounter = enemyStats.timeBetweenAttacks;
				_animator.SetBool(IsAttacking, true);
				_castleHealth.TakeDamage(enemyStats.damagePerAttack);
			}
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		// Reset the Enemy so he can Spawn with full life ...
		public void ResetEnemy()
		{
			// Reset Health back to MaxHealth
			health = enemyStats.maxHealth;
			_isDead = false;
			_currentWayPoint = 0;
			_reachedTheEnd = false;
			_attackCounter = 0f;
			speedMod = 1f; // back to normal speed
			_burnDamage = 0f;
			_burnTimer = 0f;
			// flug Gegner werden auf Ihrer Höhe resetet
			if (isFlying) transform.position +=  Vector3.up * flyHeight;
			_collider.enabled = true;
			if (_animator == null) return; 
			_animator.SetBool(IsAttacking, false);
			_animator.SetBool(IsDeath, false);
			_animator.SetBool(IsDancing, false);
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
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void Dance()
		{
			_reachedTheEnd = true;
			speedMod = 0f;
			_attackCounter = float.MaxValue;
			_animator.SetBool(IsAttacking, false);
			_animator.SetBool(IsDancing, true);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void Die()
		{
			_isDead = true;
			_reachedTheEnd = true;
			speedMod = 0f;
			_animator.SetBool(IsDeath, true);
			_collider.enabled = false;
			StartCoroutine(ReturnToPoolAfterDeath());
			return;

			IEnumerator ReturnToPoolAfterDeath()
			{
				yield return new WaitForSecondsRealtime(enemyStats.deathAnimationDuration);
				ReturnToPool();
			}
		}
	}
}
