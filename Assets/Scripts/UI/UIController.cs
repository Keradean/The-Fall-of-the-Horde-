using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class UIController : MonoBehaviour
{
    [FormerlySerializedAs("PanelWinScreen")] [SerializeField] public GameObject panelWinScreen;
    [FormerlySerializedAs("PanelLoseScreen")] [SerializeField] public GameObject panelLoseScreen;
    [FormerlySerializedAs("PanelPlaceTower")] [SerializeField] public GameObject panelPlaceTower;
    [FormerlySerializedAs("PauseScreen")] [SerializeField] public GameObject pauseScreen;
    [FormerlySerializedAs("UpgradeScreen")] [SerializeField] public GameObject upgradeScreen;

	[Header("Display Tower Cost")]
    [SerializeField] private Tower  ballistaTower;
    [SerializeField] private Tower  iceTower;
    [SerializeField] private Tower  fireTower;
   // [SerializeField] Tower  _insertnextTowerHere????;	

	[Header("Display Tower Upgrade")]
	[SerializeField] private TMP_Text descriptionTMP;
	[SerializeField] private TMP_Text costTMP;
	[SerializeField] private Transform displayPoint;
	private GameObject _towerDisplay; 
	//[SerializeField] 

	[Header("Display Tower Upgrade Cost")]


    public static UIController Instance;
    public TMP_Text goldTMP;
    [FormerlySerializedAs("BallistaTMP")] public TMP_Text ballistaTMP;
    [FormerlySerializedAs("IceTowerTMP")] public TMP_Text iceTowerTMP;
    [FormerlySerializedAs("FireTowerTMP")] public TMP_Text fireTowerTMP;
    
    private InputSystem_Actions _inputActions;
	private Tower _selectedTower;

    private void Awake()
    {
        Instance = this;
        _inputActions = new InputSystem_Actions();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		// Display Tower Costs
        ballistaTMP.text = ballistaTower.towerStats.cost.ToString();
        iceTowerTMP.text = iceTower.towerStats.cost.ToString();
        fireTowerTMP.text = fireTower.towerStats.cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UIController.Instance.goldTMP.text = GoldManager.Instance.currentGold.ToString();
    }

    private void OnEnable()
    {
        _inputActions.UI.Enable();
        _inputActions.UI.Pause.performed += OnPause;
    }

    private void OnDisable()
    {
        _inputActions.UI.Pause.performed -= OnPause;
        _inputActions.UI.Disable();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        PauseUnpause();
    }

	#region Buttons
    public void PauseUnpause()
    {
        if (TowerManager.Instance.isPlacing)
        {
            TowerManager.Instance.DontPlaceTheTower();return;
        }
        if (pauseScreen.activeSelf == false)
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
	
	public void Retry()
	{
		SceneManager.LoadScene("TestScene");
	}	

	public void Resume()
	{
		pauseScreen.SetActive(false);
		Time.timeScale = 1f;
	}

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowUpgradeUI(Tower tower)
    {
        _selectedTower = tower;
        upgradeScreen.SetActive(true);
		
		descriptionTMP.text = tower.towerStats.description;
		costTMP.text = tower.towerStats.cost.ToString(); 

		if(_towerDisplay != null) Destroy(_towerDisplay); 
		_towerDisplay = Instantiate(tower.towerPrefab, displayPoint.position, Quaternion.identity);

    }   
    
    public void HideUpgradeUI()
    {
        _selectedTower = null;
        upgradeScreen.SetActive(false);
		if(_towerDisplay != null) Destroy(_towerDisplay);
    }

    public void OnUpgradeClick()
    {
        if (_selectedTower == null) return;
        if (GoldManager.Instance.SpendGold(_selectedTower.towerStats.cost)) _selectedTower.Upgrade();
		{
			//if(selectedTower.TryGetComponent<SlowdownTower>(out SlowdownTower slow)) slow.Upgrade();
			//else
			//{
			_selectedTower.Upgrade();
			//}
		}

        HideUpgradeUI();
    }

	public void OnSellClick()
	{
		if (_selectedTower == null) return; 

		// Get the Half of your Money back
       GoldManager.Instance.AddGold(_selectedTower.towerStats.cost / 2);
		// destry tower
		Destroy(_selectedTower.gameObject);
		HideUpgradeUI();
	}
	#endregion
}
