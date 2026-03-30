using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;
    
    public int currentGold;
    
    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
    }

    public bool SpendGold(int amount)
    {
        bool canSpendGold = false;
        
        if (amount <= currentGold)
        {
            canSpendGold = true;

            Debug.Log("Spent" + amount);
            currentGold -=  amount;
        }
        
        return canSpendGold;
    }
}
