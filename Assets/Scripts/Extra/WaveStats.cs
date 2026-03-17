using UnityEngine;

[CreateAssetMenu(menuName = "Spawner/WaveStats", fileName = "WaveStats")]
public class WaveStats : ScriptableObject
{
    public Enemy enemyPrefab;
    public int numberOfEnemies;
    public float timeBetweenSpawns;
    public float timeBetweenWaves; 
}
