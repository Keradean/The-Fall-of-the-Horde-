using UnityEngine;

[CreateAssetMenu(menuName = "Tower/TowerBasicStats", fileName="TowerBasicStats")]
public class TowerStats : ScriptableObject
{
    [Header("Tower Stats")]
    public float range;
    public float timeBetweenAttacks;

    [Header("Cost")]
    public int cost;
}
