using Manager;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// Fürs Canva Slider/ Healthbar

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [FormerlySerializedAs("_enemyStats")] [SerializeField] private EnemyStats enemyStats;
        [FormerlySerializedAs("_enemy")] [SerializeField] private Enemy enemy;
        [SerializeField] private Slider enemyHealthBar;
 
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnEnable()
        {
            enemy.health = enemyStats.maxHealth;
            
            enemyHealthBar.maxValue = enemyStats.maxHealth;
            enemyHealthBar.value = enemy.health;

            LevelManager.Instance.activeEnemies.Add(this);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void OnDisable()
        {
            LevelManager.Instance.activeEnemies.Remove(this);
        } 
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void TakeDamage(float damaged)
        {
            enemy.health -=  damaged;
            if (enemy.health <= 0f)
            {
                enemy.health = 0f;
                //ToDO
                //Animation
                GoldManager.Instance.AddGold(enemyStats.goldOnDeath);
                enemy.ReturnToPool();
               // AudioManager.Instance.PlaySfx(0);
                return;
            }
            enemyHealthBar.value = enemy.health;
        }
    }
}
