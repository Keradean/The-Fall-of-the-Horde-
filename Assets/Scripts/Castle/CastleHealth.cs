using Extra;
using UnityEngine;
using UnityEngine.UI;

// Fürs Canva Slider/ Healthbar

namespace Castle
{
    public class CastleHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private CastleStats castleStats;
        [SerializeField] private Slider castleHealthBar;

        public Transform[] pointsOfAttack;

        public bool isInitialized;
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void Start()
        {
            castleStats.health = castleStats.maxHealth;
            castleHealthBar.maxValue = castleStats.maxHealth;
            castleHealthBar.value = castleStats.health;
            isInitialized = true;
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
                
            }
            castleHealthBar.value = castleStats.health;
        }
    }
}
