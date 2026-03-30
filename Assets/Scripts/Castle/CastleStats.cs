using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu( menuName = "Building/Castle", fileName = "Castle Stats")]
public class CastleStats : ScriptableObject
{
    [FormerlySerializedAs("Health")] [Header("Health")]
    public float health;
    public float maxHealth;
}
