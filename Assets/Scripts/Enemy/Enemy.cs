using UnityEngine;
using UnityEngine.Pool;
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;


    public Path _path;
	private int currentWayPoint;
	private bool reachedTheEnd;
	
	private float attackCounter;
	private CastleHealth _castleHealth;


	private IObjectPool<Enemy> enemyPool;
	public void SetPool(IObjectPool<Enemy> pool)
	{
		enemyPool = pool;
	}
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _path = FindFirstObjectByType<Path>();
		_castleHealth = FindFirstObjectByType<CastleHealth>();
    }

    // Update is called once per frame
    void Update()
    {
 			    if (!reachedTheEnd)
	    {
		    transform.position = Vector3.MoveTowards(transform.position, _path.wayPoints[currentWayPoint].position, enemyStats.moveSpeed * Time.deltaTime );
		    transform.LookAt(_path.wayPoints[currentWayPoint].position);
		    if(Vector3.Distance(transform.position, _path.wayPoints[currentWayPoint].position) < .01f)
		    {
			    currentWayPoint++;
			    if(currentWayPoint >= _path.wayPoints.Length)
			    {
				    reachedTheEnd = true;
			    }
		    }
	    }
		else
		{
			attackCounter -= Time.deltaTime;
			if(attackCounter <= 0)
			{
				attackCounter = enemyStats.timeBetweenAttacks;
				_castleHealth.TakeDamage(enemyStats.damagePerAttack);
			}
    	}
	}
	
	public void ResetEnemy()
	{
		// Reset Health back to MaxHealth
		enemyStats.health = enemyStats.maxHealth;
		// ToDo Reset other things that has to be reset!!!
	}

}
