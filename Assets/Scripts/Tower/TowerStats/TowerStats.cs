using UnityEngine;

namespace Tower.TowerStats
{
    [CreateAssetMenu(menuName = "Tower/TowerBasicStats", fileName="TowerBasicStats")]
    public class TowerStats : ScriptableObject
    {
        [Header("Tower Stats")]
        public float range;
        public float timeBetweenAttacks;

        [Header("Text")]
        [TextArea] public string description;    

        [Header("Cost")]
        public int cost;
    }
}
