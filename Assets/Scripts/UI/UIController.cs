using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Extra;
using Tower.Upgrade;

namespace UI
{
	public class UIController : MonoBehaviour
	{
		[FormerlySerializedAs("PanelWinScreen")] [SerializeField] public GameObject panelWinScreen;
		[FormerlySerializedAs("PanelLoseScreen")] [SerializeField] public GameObject panelLoseScreen;
		[FormerlySerializedAs("PanelPlaceTower")] [SerializeField] public GameObject panelPlaceTower;
		[FormerlySerializedAs("PauseScreen")] [SerializeField] public GameObject pauseScreen;
		[FormerlySerializedAs("UpgradeScreen")] [SerializeField] public GameObject upgradeScreen;
		[Header("Wave Display")]
		[SerializeField] private TMP_Text waveTMP;
		[SerializeField] private Spawner enemiesSpawner;
		[Header("Display Tower Cost")]
		[SerializeField] private Tower.Tower  ballistaTower;
		[SerializeField] private Tower.Tower  iceTower;
		[SerializeField] private Tower.Tower  fireTower;
		[SerializeField] private Tower.Tower  cannonTower;
		// [SerializeField] Tower insert next Tower Here????;	
		[Header("Display Tower Upgrade")]
		[SerializeField] private TMP_Text descriptionTMP;
		[SerializeField] private TMP_Text costTMP;
		[SerializeField] private Transform displayPoint;
		[SerializeField] private UnityEngine.UI.Button upgradeButton;
		private GameObject _towerDisplay; 
		//[SerializeField] 
		[Header("Display Tower Upgrade Cost")]
		public static UIController Instance;
		public TMP_Text goldTMP;
		[FormerlySerializedAs("BallistaTMP")] public TMP_Text ballistaTMP;
		[FormerlySerializedAs("IceTowerTMP")] public TMP_Text iceTowerTMP;
		[FormerlySerializedAs("FireTowerTMP")] public TMP_Text fireTowerTMP;
		[FormerlySerializedAs("FireTowerTMP")] public TMP_Text cannonTowerTMP;
    
		private InputSystem_Actions _inputActions;
		private Tower.Tower _selectedTower;
		private UpgradeTower _selectedUpgradeTower;
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void Awake()
		{
			Instance = this;
			_inputActions = new InputSystem_Actions();
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		private void Start()
		{
			// Display Tower Costs
			ballistaTMP.text = ballistaTower.towerStats.cost.ToString();
			iceTowerTMP.text = iceTower.towerStats.cost.ToString();
			fireTowerTMP.text = fireTower.towerStats.cost.ToString();
			cannonTowerTMP.text = cannonTower.towerStats.cost.ToString();
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		// Update is called once per frame
		private void Update()
		{
			UIController.Instance.goldTMP.text = GoldManager.Instance.currentGold.ToString();
			waveTMP.text = "Wave" + enemiesSpawner.CurrentWave + "/" + enemiesSpawner.TotalWaves;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void OnEnable()
		{
			_inputActions.UI.Enable();
			_inputActions.UI.Pause.performed += OnPause;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void OnDisable()
		{
			_inputActions.UI.Pause.performed -= OnPause;
			_inputActions.UI.Disable();
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void OnPause(InputAction.CallbackContext context)
		{
			PauseUnpause();
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void PauseUnpause()
		{
			if (TowerManager.Instance.isPlacing)
			{
				TowerManager.Instance.DontPlaceTheTower();return;
			}
			if (!pauseScreen.activeSelf)
			{
				pauseScreen.SetActive(true);
				Time.timeScale = 0f; 
			}
			else
			{
				pauseScreen.SetActive(false);
				Time.timeScale = 1f;	
			}
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void Retry()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}	
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void Resume()
		{
			pauseScreen.SetActive(false);
			Time.timeScale = 1f;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void LevelSelect()
		{
			SceneManager.LoadScene("LevelSelect");
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void MainMenu()
		{
			SceneManager.LoadScene("MainMenu");
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void OnUpgradeClick()
		{
			_selectedUpgradeTower?.OnUpgradeButton();
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void OnSellClick()
		{
			_selectedUpgradeTower?.OnSell();
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void ShowUpgradeUI(Tower.Tower tower)
		{
			_selectedTower = tower;
			upgradeScreen.SetActive(true);
			_selectedUpgradeTower = tower.GetComponent<UpgradeTower>();
			descriptionTMP.text = tower.towerStats.description;
			costTMP.text = tower.towerStats.upgradeCost.ToString(); 
			if(_towerDisplay != null) Destroy(_towerDisplay); 
			_towerDisplay = Instantiate(tower.towerPrefab, displayPoint.position, Quaternion.identity);
			upgradeButton.interactable = _selectedTower.CanUpgrade;
		}   
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void HideUpgradeUI()
		{
			_selectedTower = null;
			upgradeScreen.SetActive(false);
			if(_towerDisplay != null) Destroy(_towerDisplay);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void FastTime()
		{
			Time.timeScale = 2f;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void ResumeTime()
		{
			Time.timeScale = 1f;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void SlowTime()
		{
			Time.timeScale = .5f;
		}
	}
}
