using Projectile;
using UnityEngine;

namespace Tower.TowerStats
{
    [CreateAssetMenu(menuName = "Tower/CastleDefenceStats", fileName = "CastleDefenceStats")]
    public class CastleDefenceStats : ProjectileStats
    {
        [Header("Castle Defence")] 
        public float aoeDamage; 
    }
}