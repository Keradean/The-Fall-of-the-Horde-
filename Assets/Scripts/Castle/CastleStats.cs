using UnityEngine;
using UnityEngine.Serialization;

namespace Castle
{
    [CreateAssetMenu( menuName = "Building/Castle", fileName = "Castle Stats")]
    public class CastleStats : ScriptableObject
    {
        [FormerlySerializedAs("Health")] [Header("Health")]
        public float health;
        public float maxHealth;
    }
}
