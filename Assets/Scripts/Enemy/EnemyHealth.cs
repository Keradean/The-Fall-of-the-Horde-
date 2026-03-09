using UnityEngine;
using UnityEngine.UI; // Fürs Canva Slider/ Healthbar

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Slider enemyHealthBar;
    

    void Start()
    {
        _enemy.Health = _enemyStats.maxHealth;
            
        enemyHealthBar.maxValue = _enemyStats.maxHealth;
        enemyHealthBar.value = _enemy.Health;
    }
    
    public void TakeDamage(float damaged)
    {
        _enemy.Health -=  damaged;
        if (_enemy.Health <= 0f)
        {
            _enemy.Health = 0f;

            //ToDO
            //Animation better than this SetActive!!
            gameObject.SetActive(false);
            
            Debug.Log("Er ist gestorben!!!");
        }
        enemyHealthBar.value = _enemy.Health;
    }
}
