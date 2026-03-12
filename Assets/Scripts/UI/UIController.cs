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
    
}
