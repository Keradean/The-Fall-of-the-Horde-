using UnityEngine;
using UnityEngine.Pool;
public class Spawner : MonoBehaviour
{
    [Header("Spawn Time")]
    [SerializeField] private Transform  spawnPoint;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] public int numberOfSpawns;

    [Header("Enemy Prefabs")] 
    [SerializeField] private Enemy enemyPrefab;
    
    [Header("Castle & Path Reference")]
    [SerializeField] private CastleHealth _castleHealth;
    [SerializeField] private CastleStats _castleStats;
    [SerializeField] private Path _path;
    

    private ObjectPool<Enemy> enemyPool;
    private float spawnTimer;

    private void Awake()
    {
        enemyPool = new ObjectPool<Enemy>(
            CreateEnemy,
            OnTakeFromPool,
            OnReturnFromPool,
            OnDestroyEnemy,
            collectionCheck: true,    // helps catch double-release mistakes
            defaultCapacity: 20,     // durschnittlich 20 Gegner auf der Map
            maxSize: 50             // extreme Welle, 50 Gegner maximal
        );
    }
    
    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab);			 	// spawne den Gegner
        enemy.Setup(_castleHealth, _path);     				// die Referenz wo und wohin er gehen soll
        enemy.SetPool(enemyPool);							// der Gegner wird wieder seinem Pool zugeordnet
		enemy.transform.position = spawnPoint.position; 	// setze die Position des Gegner auf die des SpawnPoints
        return enemy; 
    }

    private void OnTakeFromPool(Enemy enemy)
    {
		enemy.transform.position = spawnPoint.position;
        enemy.gameObject.SetActive(true);
        enemy.ResetEnemy(); // Setzt den Gegner auf den Startzustand zurück
    }

    private void OnReturnFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
		// keine neuen Spawns mehr wenn die Burg zerstört ist 
		if(numberOfSpawns > 0 && _castleStats.Health > 0)
		{
			// Prüfen ob der Timer abgelaufen ist und weniger gespawnte Gegner da sind (limit)
        	if (Time.time > spawnTimer) 
       		{
            //Spawn Enemy
            enemyPool.Get(); // Gegner aus dem Pool holen
			numberOfSpawns --; // Hioer wird Runtergezählt
            spawnTimer = Time.time +  timeBetweenSpawns; // Den Timer Zurück setzen    
    	    }
		}

    }
}
