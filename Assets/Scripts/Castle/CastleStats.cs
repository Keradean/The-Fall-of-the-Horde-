using UnityEngine;

[CreateAssetMenu( menuName = "Building/Castle", fileName = "Castle Stats")]
public class CastleStats : ScriptableObject
{
    [Header("Health")]
    public float Health;
    public float maxHealth;
}
