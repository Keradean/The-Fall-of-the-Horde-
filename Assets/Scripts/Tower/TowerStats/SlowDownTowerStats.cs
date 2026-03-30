using UnityEngine;

namespace Tower.TowerStats
{
    [CreateAssetMenu(menuName = "Tower/SlowDownTowerStats", fileName = "Frost Tower Level")]
    public class SlowDownTowerStats : TowerStats
    {
        [Header("SlowDownAmount")]
        public float slowDownAmount;
    }
}
