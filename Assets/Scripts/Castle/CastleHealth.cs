using Extra;
using Manager;
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
        private float _damageSoundCooldown;
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void Start()
        {
            castleStats.health = castleStats.maxHealth;
            castleHealthBar.maxValue = castleStats.maxHealth;
            castleHealthBar.value = castleStats.health;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update()
        {
            if(_damageSoundCooldown > 0) _damageSoundCooldown -= Time.deltaTime;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void TakeDamage(float damaged)
        {
            castleStats.health -=  damaged;
            if (_damageSoundCooldown <= 0)
            {
                AudioManager.Instance?.PlaySfx(2);
                _damageSoundCooldown = 1f;
            }
            if (castleStats.health <= 0f)
            {
                castleStats.health = 0f;
                AudioManager.Instance?.PlaySfx(3);
                gameObject.SetActive(false);
            }
            castleHealthBar.value = castleStats.health;
        }
    }
}
