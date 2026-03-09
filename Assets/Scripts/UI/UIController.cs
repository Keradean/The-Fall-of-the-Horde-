using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    public static UIController instance;
    
    public TMP_Text goldTMP;

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
        UIController.instance.goldTMP.text = GoldManager.instance.currentGold.ToString();
    }
    
}
