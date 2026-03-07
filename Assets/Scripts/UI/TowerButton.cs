using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TowerButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Tower placeTower;

    [SerializeField] string towerName;
    [SerializeField] private TextMeshProUGUI towerDisplayText;
    

    public void SelectTower()
    {
        TowerManager.instance.PlaceTheTower(placeTower);
        Debug.Log("Klick mich hart!!!");
    }
    
    public void OnPointerEnter(PointerEventData eventData)
	{	
		towerDisplayText.text = towerName;
	}    
}
