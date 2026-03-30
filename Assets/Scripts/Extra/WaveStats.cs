using UnityEngine;

namespace Extra
{
    [CreateAssetMenu(menuName = "Spawner/WaveStats", fileName = "WaveStats")]
    public class WaveStats : ScriptableObject
    {
        public Enemy.Enemy enemyPrefab;
        public int numberOfEnemies;
        public float timeBetweenSpawns;
        public float timeBetweenWaves;
    }
}
