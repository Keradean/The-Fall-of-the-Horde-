using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool levelActive;
    private bool levelComplete;
    
    public Castle _castle;
    
    public List<EnemyHealth> activeEnemies = new List<Enemyhealth>();
    
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
