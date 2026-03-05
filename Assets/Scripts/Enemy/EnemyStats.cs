using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyStats", fileName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{ 
    [Header("MoveSpeed")]
    public float moveSpeed;

    [Header("Attack")]
    public float timeBetweenAttacks;
    public float damagePerAttack;
    
    [Header("Health")]
    public float Health;
    public float maxHealth;
}
