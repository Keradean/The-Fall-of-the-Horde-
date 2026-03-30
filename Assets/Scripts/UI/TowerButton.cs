using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UI
{
    public class TowerButton : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private Tower.Tower placeTower;
        [FormerlySerializedAs("_towerManager")] [SerializeField] private TowerManager towerManager;

        [SerializeField] string towerName;
        [SerializeField] private TextMeshProUGUI towerDisplayText;
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void SelectTower()
        {
            TowerManager.Instance.PlaceTheTower(placeTower);
            Debug.Log("Klick mich hart!!!");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnPointerEnter(PointerEventData eventData)
        {	
            towerDisplayText.text = towerName;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void DeselectTower()
        {
            TowerManager.Instance.DontPlaceTheTower();
        }
    }
}
