using UnityEngine;
using UnityEngine.UI; // Fürs Canva Slider/ Healthbar

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Slider enemyHealthBar;
    

    void OnEnable()
    {
        _enemy.Health = _enemyStats.maxHealth;
            
        enemyHealthBar.maxValue = _enemyStats.maxHealth;
        enemyHealthBar.value = _enemy.Health;

		LevelManager.instance.activeEnemies.Add(this);
    }
	void OnDisable()
	{
		LevelManager.instance.activeEnemies.Remove(this);
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

			GoldManager.instance.AddGold(_enemyStats.goldOnDeath);
            
            Debug.Log("Er ist gestorben!!!");
        }
        enemyHealthBar.value = _enemy.Health;
    }
}
