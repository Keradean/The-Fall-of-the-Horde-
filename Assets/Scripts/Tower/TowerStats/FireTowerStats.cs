using UnityEngine;

[CreateAssetMenu(menuName = "Tower/FireTowerStats", fileName = "FireTowerStats")]
public class FireTowerStats : ProjectileStats
{
    [Header("Burn")]
    public float burnDamage;
    public float burnDuration;

}
