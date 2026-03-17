using UnityEngine;
using UnityEngine.Pool;
public class Spawner : MonoBehaviour
{
    [Header("Wave")]
	[SerializeField] private WaveStats[] wave;
	[SerializeField] private Enemy bossPrefab; 
    
    [Header("Castle & Path Reference")]
    [SerializeField] private Transform  spawnPoint;
    [SerializeField] private CastleHealth _castleHealth;
    //[SerializeField] private CastleStats _castleStats;
    [SerializeField] private Path _path;
    
	private int currentWave = 0;
	private int enemiesLeftToSpawn;
	private float spawnTimer;
	private float waveTimer;
	private bool waveActive;
	private bool bossSpawned; 

    private ObjectPool<Enemy> enemyPool;


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

	private void Start()
	{
		StartWave();
	}

	private void StartWave()
	{
		enemiesLeftToSpawn = wave[currentWave].numberOfEnemies;
		waveActive = true; 
		Debug.Log("Start For The Horde!!");
	}

    // Update is called once per frame
    void Update()
    {
		if(!waveActive)
		{
			waveTimer -= Time.deltaTime;
			if(waveTimer <= 0) // ist die Zeit abgelaufen? 
			{
				// Alle Wellen sind durch also Spawne den Endgegner!
				if(currentWave >= wave.Length)
				{
					if(!bossSpawned)
					{
						bossSpawned = true;
						Enemy boss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
						boss.Setup(_castleHealth, _path);
						Debug.Log(" Der Endgegner ist in the House!!!");
					}
					return;
				}
				StartWave(); // Starte die nächste Welle!!!
			}
			return;
		}
		// Spawne Gegener
		if(enemiesLeftToSpawn > 0 && Time.time > spawnTimer)
		{
			enemyPool.Get(); // hole Gegner aus dem Pool raus
			enemiesLeftToSpawn--; // einen weniger Spawnen	
			spawnTimer = Time.time + wave[currentWave].timeBetweenSpawns; // Setze den timer neu 
		}
		// Die Welle beenden und den Boss Spawnen lassen
		if(enemiesLeftToSpawn <= 0)
		{
			waveActive = false; // stop die Welle
			currentWave++; // bereite die nächste Welle vor
			waveTimer = currentWave < wave.Length ? wave[currentWave].timeBetweenWaves : 3f; 
		}
		/*
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
		}*/
    }
    
    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(wave[currentWave].enemyPrefab);			 	// spawne den Gegner
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
	
	public bool IsFinished()
	{
		return bossSpawned && LevelManager.instance.activeEnemies.Count == 0;
	}

}
