using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private CastleStats castleStats;
    [SerializeField] private Spawner enemiesSpawner;
    public static LevelManager instance;
    public bool levelActive;
    private bool levelComplete;
    
    public List<EnemyHealth> activeEnemies = new List<EnemyHealth>();
    
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelActive)
        {
            if (castleStats.Health <= 0)
            {
                levelActive = false;
                levelComplete = false;
               // Debug.Log("You Lose....Looser...!!");
               UIController.instance.PanelLoseScreen.SetActive(true);
               UIController.instance.PanelPlaceTower.SetActive(false);
            }
            else
            if (activeEnemies.Count == 0 && enemiesSpawner.numberOfSpawns == 0)
            {
                levelActive = false;
                levelComplete = true;
                //Debug.Log("..Ja Krass du hast gewonnen...");
                UIController.instance.PanelWinScreen.SetActive(true);
            }
        }
        
    }
}
