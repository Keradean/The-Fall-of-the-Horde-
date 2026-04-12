using System.Collections.Generic;
using Castle;
using Enemy;
using Extra;
using UI;
using UnityEngine;

namespace Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private CastleStats castleStats;
        [SerializeField] private Spawner enemiesSpawner;
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public bool levelActive;
        private bool _levelComplete; // eventuell für Sterne vergabe oder nächstes level freischalten verwenden
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public List<EnemyHealth> activeEnemies = new List<EnemyHealth>();
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void Start()
        {
            levelActive = true;
            AudioManager.Instance?.PlayBGM();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update()
        {
            if (!levelActive) return;
            if (castleStats.health <= 0)
            {
                levelActive = false;
                _levelComplete = false;
                UIController.Instance.panelLoseScreen.SetActive(true);
                UIController.Instance.panelPlaceTower.SetActive(false);
                foreach (var enemy in activeEnemies) enemy.GetComponent<Enemy.Enemy>().Dance();
                {
                    
                }
            }
            else
            if (activeEnemies.Count == 0 && enemiesSpawner.IsFinished())
            {
                levelActive = false;
                _levelComplete = true;
                UIController.Instance.panelWinScreen.SetActive(true);
            }

        }
    }
}
