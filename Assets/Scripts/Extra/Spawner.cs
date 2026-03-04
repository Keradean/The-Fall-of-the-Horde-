using UnityEngine;
using UnityEngine.Pool;
public class Spawner : MonoBehaviour
{
    [Header("Spawn Time")]
    [SerializeField] private Transform[]  spawnPoints;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private int numberOfSpawns;

    [Header("Enemy Prefabs")] 
    [SerializeField] private Enemy enemyPrefab;
    

    private ObjectPool<Enemy> enemyPool;
    private float spawnTimer;

    private void Awake()
    {
        enemyPool = new ObjectPool<Enemy>(CreateEnemy);
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
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.SetPool(enemyPool);
        return enemy; 
    }

    private void OnTakeFromPool(Enemy enemy)
    {
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
        // Prüfen ob der Timer abgelaufen ist und weniger gespawnte Gegner da sind (limit)
        if (Time.time > spawnTimer && enemyPool.CountActive < numberOfSpawns) 
        {
            //Spawn Enemy
            enemyPool.Get(); // Gegner aus dem Pool holen
            spawnTimer = Time.time +  timeBetweenSpawns; // Den Timer Zurück setzen
        }
    }
}
