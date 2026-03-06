using UnityEngine;

public class TowerButton : MonoBehaviour
{
    public Tower placeTower;

    public void SelectTower()
    {
        TowerManager.instance.PlaceTheTower(placeTower);
        Debug.Log("Klick mich hart!!!");
    }
}
