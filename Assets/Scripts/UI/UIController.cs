using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] public GameObject PanelWinScreen;
    [SerializeField] public GameObject PanelLoseScreen;
    [SerializeField] public GameObject PanelPlaceTower;
    [SerializeField] public GameObject PauseScreen;
    [SerializeField] public GameObject UpgradeScreen;
    private Tower selectedTower;

    public static UIController instance;
    public TMP_Text goldTMP;
    
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        instance = this;
        inputActions = new InputSystem_Actions();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIController.instance.goldTMP.text = GoldManager.instance.currentGold.ToString();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Pause.performed += OnPause;
    }

    private void OnDisable()
    {
        inputActions.UI.Pause.performed -= OnPause;
        inputActions.UI.Disable();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        PauseUnpause();
    }

    public void PauseUnpause()
    {
        if (TowerManager.instance.isPlacing)
        {
            TowerManager.instance.DontPlaceTheTower();return;
        }
        if (PauseScreen.activeSelf == false)
        {
            PauseScreen.SetActive(true);
            Time.timeScale = 0f; 
        }
        else
        {
            PauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
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
        selectedTower = tower;
        UpgradeScreen.SetActive(true);

    }   
    
    public void HideUpgradeUI()
    {
        selectedTower = null;
        UpgradeScreen.SetActive(false);

    }

    public void OnUpgradeClick()
    {
        if (selectedTower == null) return;
        if (GoldManager.instance.SpendGold(selectedTower.towerStats.cost)) selectedTower.Upgrade();
		{
			if(selectedTower.TryGetComponent<SlowdownTower>(out SlowdownTower slow)) slow.Upgrade();
			else
			{
			selectedTower.Upgrade();
			}
		}

        HideUpgradeUI();
    }
}
