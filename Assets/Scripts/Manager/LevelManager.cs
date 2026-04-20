using System.Collections.Generic;
using Castle;
using Enemy;
using Extra;
using UI;
using Tower;
using UnityEngine;

namespace Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private CastleStats castleStats;
        [SerializeField] private Spawner enemiesSpawner;
        
        protected override bool PersistAcrossScenes => false;
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public bool levelActive;
        private bool _levelComplete; //eventuell für Sterne vergabe oder nächstes level freischalten verwenden
        private bool _initialized;
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public List<EnemyHealth> activeEnemies = new List<EnemyHealth>();
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private new void Awake()
        {
            if(Instance != null) Destroy(Instance.gameObject);
            base.Awake();
        }        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void Start()
        {
            castleStats.health = castleStats.maxHealth;
            levelActive = true;
            _initialized = false;
            AudioManager.Instance?.PlayBGM();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update()
        {
            if (!_initialized)
            {
                _initialized = true;
                return;
            }
            if (!levelActive) return;
            
            if (castleStats.health <= 0)
            {
                levelActive = false;
                _levelComplete = false;
                var towers = FindObjectsByType<Tower.Tower>(FindObjectsSortMode.None);
                foreach(var tower in towers) Destroy(tower.gameObject);
                UIController.Instance.panelLoseScreen.SetActive(true);
                UIController.Instance.panelPlaceTower.SetActive(false);
                var enemies = new List<EnemyHealth>(activeEnemies);
                foreach (var enemy in activeEnemies) enemy.GetComponent<Enemy.Enemy>().Dance();
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
