using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;

	[HideInInspector] public float Health;
	[HideInInspector] public float speedMod = 1f;

    public Path _path;
	private int currentWayPoint;
	private bool reachedTheEnd;
	
	private float attackCounter;
	private CastleHealth _castleHealth;

	public int chooseAPointOfAttack;


	private IObjectPool<Enemy> enemyPool;

	private float burnTimer = 0f;
	private float burnDamage = 0f; 


	public void SetPool(IObjectPool<Enemy> pool)
	{
		enemyPool = pool;
	}
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		if(_path == null)
		{
			_path = FindFirstObjectByType<Path>();
		}

        if(_castleHealth == null)
		{
			_castleHealth = FindFirstObjectByType<CastleHealth>();
		}

    }

    // Update is called once per frame
    void Update()
    {
		MoveAndAttack();

		if(burnTimer > 0)
		{
			burnTimer -= Time.deltaTime;
			EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
			enemyHealth.TakeDamage(burnDamage * Time.deltaTime);
		}
	}

	private void MoveAndAttack()
	{	
		if (!reachedTheEnd)
	    {
		    transform.position = Vector3.MoveTowards(transform.position, _path.wayPoints[currentWayPoint].position, enemyStats.moveSpeed * Time.deltaTime * speedMod );
		    transform.LookAt(_path.wayPoints[currentWayPoint].position);
		    if(Vector3.Distance(transform.position, _path.wayPoints[currentWayPoint].position) < .01f)
		    {
			    currentWayPoint++;
			    if(currentWayPoint >= _path.wayPoints.Length)
			    {
				    reachedTheEnd = true;
					chooseAPointOfAttack = Random.Range(0, _castleHealth.PointsOfAttack.Length);
			    }
		    }
	    }
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, _castleHealth.PointsOfAttack[chooseAPointOfAttack].position, enemyStats.moveSpeed * Time.deltaTime);
			attackCounter -= Time.deltaTime;
			if(attackCounter <= 0)
			{
				attackCounter = enemyStats.timeBetweenAttacks;
				_castleHealth.TakeDamage(enemyStats.damagePerAttack);
			}
    	}
		
	}

	// Reset the Enemy so he can Spawn with full life ...
	public void ResetEnemy()
	{
		// Reset Health back to MaxHealth
		Health = enemyStats.maxHealth;
		// ToDo Reset other things that has to be reset!!!
		currentWayPoint = 0;
		reachedTheEnd = false;
		attackCounter = 0f;
		speedMod = 1f; // back to normal speed
		burnDamage = 0f;
		burnTimer = 0f;
		
	}
	
	public void Setup(CastleHealth newCastle, Path newPath)
	{
		_path = newPath;
		_castleHealth = newCastle;
	}

	public void SetOnFire(float damaged, float duration)
	{
		burnDamage = damaged;
		burnTimer = duration; 
		
	
	}
}
