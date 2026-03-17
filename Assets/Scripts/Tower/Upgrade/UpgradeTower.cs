using UnityEngine;
using TMPro;

public class UpgradeTower : MonoBehaviour
{
    private Tower _tower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUpgradeButton()
    {
        if (GoldManager.instance.SpendGold(_tower.towerStats.cost)) _tower.Upgrade();
    }
    
    public void OnSell()
    {
        // To Do: Gold zurückgeben lassen muss zerstört werden
        Debug.Log("Her mit den Dollares");
    }
}
