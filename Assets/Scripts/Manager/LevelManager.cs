using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private CastleStats castleStats;
    [SerializeField] private Spawner enemiesSpawner;
    public static LevelManager Instance;
    public bool levelActive;
    private bool _levelComplete; // eventuell für Sterne vergabe oder nächstes level freischalten verwenden
    
    public List<EnemyHealth> activeEnemies = new List<EnemyHealth>();
    
    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelActive = true;
        
        AudioManager.Instance?.PlayBGM();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelActive)
        {
            if (castleStats.health <= 0)
            {
                levelActive = false;
                _levelComplete = false;
               // Debug.Log("You Lose....Looser...!!");
               UIController.Instance.panelLoseScreen.SetActive(true);
               UIController.Instance.panelPlaceTower.SetActive(false);
            }
            else
            if (activeEnemies.Count == 0 && enemiesSpawner.IsFinished())
            {
                levelActive = false;
                _levelComplete = true;
                //Debug.Log("..Ja Krass du hast gewonnen...");
                UIController.Instance.panelWinScreen.SetActive(true);
            }
        }
        
    }
}
