using UnityEngine;
using UnityEngine.UI; // Fürs Canva Slider/ Healthbar

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private Slider enemyHealthBar;
    

    void Start()
    {
        _enemyStats.Health = _enemyStats.maxHealth;
            
        enemyHealthBar.maxValue = _enemyStats.maxHealth;
        enemyHealthBar.value = _enemyStats.Health;
    }
    
    public void TakeDamage(float damaged)
    {
        _enemyStats.Health -=  damaged;
        if (_enemyStats.Health <= 0f)
        {
            _enemyStats.Health = 0f;
            //ToDO
            //Animation better than this SetActive!!
            gameObject.SetActive(false);
            
            
            Debug.Log("Er ist gestorben!!!");
        }
        enemyHealthBar.value = _enemyStats.Health;
    }
}
