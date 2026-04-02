using Extra;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// Fürs Canva Slider/ Healthbar

namespace Castle
{
    public class CastleHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private CastleStats castleStats;
        [SerializeField] private Slider castleHealthBar;

        [FormerlySerializedAs("PointsOfAttack")] public Transform[] pointsOfAttack;
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void Start()
        {
            castleStats.health = castleStats.maxHealth;
            castleHealthBar.maxValue = castleStats.maxHealth;
            castleHealthBar.value = castleStats.health;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void TakeDamage(float damaged)
        {
            castleStats.health -=  damaged;
            if (castleStats.health <= 0f)
            {
                castleStats.health = 0f;
                //ToDO
                //Animation better than this SetActive!!
                gameObject.SetActive(false);
                //LoseScreen();
            }
            castleHealthBar.value = castleStats.health;
        }
    }
}
