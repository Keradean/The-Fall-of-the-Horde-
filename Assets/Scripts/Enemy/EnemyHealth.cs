using Manager;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [FormerlySerializedAs("_enemyStats")] [SerializeField] private EnemyStats enemyStats;
        [FormerlySerializedAs("_enemy")] [SerializeField] private Enemy enemy;
        [SerializeField] private Slider enemyHealthBar;
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void OnEnable()
        {
            enemy.health = enemyStats.maxHealth;
            enemyHealthBar.maxValue = enemyStats.maxHealth;
            enemyHealthBar.value = enemy.health;
            if(LevelManager.Instance != null)
                LevelManager.Instance.activeEnemies.Add(this);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void OnDisable()
        {
            if(LevelManager.Instance != null)
                LevelManager.Instance.activeEnemies.Remove(this);
        } 
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void TakeDamage(float damaged)
        {
            if(enemy.health <= 0f) return; // Einmal sterben ist genug!
            
            enemy.health -=  damaged;
            if (enemy.health <= 0f)
            {
                enemy.health = 0f;
                //ToDO
                
                GoldManager.Instance.AddGold(enemyStats.goldOnDeath);
                enemy.Die();
               // AudioManager.Instance.PlaySfx(0);
            }
            enemyHealthBar.value = enemy.health;
        }
    }
}
