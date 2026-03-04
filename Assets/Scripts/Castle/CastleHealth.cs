using UnityEngine;
using UnityEngine.UI; // Fürs Canva Slider/ Healthbar

public class CastleHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private CastleStats castleStats;
    [SerializeField] private Slider castleHealthBar;

    void Start()
    {
        castleStats.Health = castleStats.maxHealth;
            
        castleHealthBar.maxValue = castleStats.maxHealth;
        castleHealthBar.value = castleStats.Health;
    }
    
    public void TakeDamage(float damaged)
    {
        castleStats.Health -=  damaged;
        if (castleStats.Health <= 0f)
        {
            castleStats.Health = 0f;
            //ToDO
            //Animation better than this SetActive!!
            gameObject.SetActive(false);
            //LoseScreen();
            
            Debug.Log("Wie fühlt es sich an zu verlieren?");
        }
        castleHealthBar.value = castleStats.Health;
    }
}
