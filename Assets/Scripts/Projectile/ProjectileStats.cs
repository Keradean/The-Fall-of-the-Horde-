using Tower.TowerStats;
using UnityEngine;

namespace Projectile
{
    [CreateAssetMenu(menuName = "Projectiles/ Projectile Stats", fileName = "ProjectileStats")]
    public class ProjectileStats : TowerStats
    {
        [Header("Damage")]
        public float damage;
    }
}
