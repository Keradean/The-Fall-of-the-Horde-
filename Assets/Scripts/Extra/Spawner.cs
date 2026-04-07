using Castle;
using Manager;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace Extra
{
	public class Spawner : MonoBehaviour
	{
		[Header("Wave")]
		[SerializeField] private WaveStats[] wave;
    
		[Header("Castle & Path Reference")]
		[SerializeField] private Transform  spawnPoint;
		[FormerlySerializedAs("_castleHealth")] [SerializeField] private CastleHealth castleHealth;
		//[SerializeField] private CastleStats _castleStats;
		[FormerlySerializedAs("_path")] [SerializeField] private Path.Path path;
    
		private int _currentWave;
		private int _enemiesLeftToSpawn;
		private float _spawnTimer;
		private float _waveTimer;
		private bool _waveActive;

		private ObjectPool<Enemy.Enemy>[] _enemyPools;

		////////////////////////////////////////////////////////////////////////////////////////////////
		private void Awake()
		{
			_enemyPools = new ObjectPool<Enemy.Enemy>[wave.Length];
			for (var i = 0; i < wave.Length; i++)
			{
				var index = i;					 // Lambda capture
				_enemyPools[index] = new ObjectPool<Enemy.Enemy>(
					()=> CreateEnemy(index),
					OnTakeFromPool,
					OnReturnFromPool,
					OnDestroyEnemy,
					collectionCheck: true,		// helps catch double-release mistakes
					defaultCapacity: 20,		// durschnittlich 20 Gegner auf der Map
					maxSize: 50					// extreme Welle, 50 Gegner maximal
				);
			}
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void Start()
		{
			StartWave();
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void StartWave()
		{
			_enemiesLeftToSpawn = wave[_currentWave].numberOfEnemies;
			_waveActive = true; 
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void Update()
		{
			if(!_waveActive)
			{
				_waveTimer -= Time.deltaTime;
				if (!(_waveTimer <= 0)) return;										// ist die Zeit abgelaufen? 
				// Alle Wellen sind durch also Spawne den Endgegner!
				if (_currentWave >= wave.Length) return;
				StartWave();														// Starte die nächste Welle!!!
				return;
			}
			// Spawne Gegner
			if(_enemiesLeftToSpawn > 0 && Time.time > _spawnTimer)
			{
				_enemyPools[_currentWave].Get();									// hole Wave aus dem Pool raus
				_enemiesLeftToSpawn--;												// einen weniger Spawnen	
				_spawnTimer = Time.time + wave[_currentWave].timeBetweenSpawns;		// Setze den timer neu 
			}
			// Die Welle beenden und den Boss Spawnen lassen
			if (_enemiesLeftToSpawn > 0) return;
			_waveActive = false;													// stop die Welle
			_currentWave++;															// bereite die nächste Welle vor
			_waveTimer = _currentWave < wave.Length ? wave[_currentWave].timeBetweenWaves : 3f;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private Enemy.Enemy CreateEnemy(int waveIndex)
		{
			var enemy = Instantiate(wave[waveIndex].enemyPrefab);	// spawne den Gegner
			enemy.Setup(castleHealth, path);     					// die Referenz wo und wohin er gehen soll
			enemy.SetPool(_enemyPools[waveIndex]);					// der Gegner wird wieder seinem Pool zugeordnet
			enemy.transform.position = spawnPoint.position; 		// setze die Position des Gegners auf die des SpawnPoints
			return enemy; 
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void OnTakeFromPool(Enemy.Enemy enemy)
		{
			enemy.transform.position = spawnPoint.position;
			enemy.ResetEnemy();										// Setzt den Gegner auf den Startzustand zurück
			enemy.gameObject.SetActive(true);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private static void OnReturnFromPool(Enemy.Enemy enemy)
		{
			enemy.gameObject.SetActive(false);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private static void OnDestroyEnemy(Enemy.Enemy enemy)
		{
			if(enemy != null) Destroy(enemy.gameObject);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////	
		public bool IsFinished()
		{
			return _currentWave >= wave.Length && !_waveActive && LevelManager.Instance.activeEnemies.Count == 0;
		}

	}
}
