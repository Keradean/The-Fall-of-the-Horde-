using Manager;
using UI;
using UnityEngine;

namespace Tower.Upgrade
{
    public class UpgradeTower : MonoBehaviour
    {
        private Tower _tower;
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private void Awake()
        {
            _tower = GetComponent<Tower>();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnUpgradeButton()
        {
            if (!_tower.CanUpgrade) return;
            if (GoldManager.Instance.SpendGold(_tower.towerStats.upgradeCost))
            {
                AudioManager.Instance?.PlaySfx(6);
                _tower.Upgrade();
                UIController.Instance.HideUpgradeUI();
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnSell()
        {
            var refund = Mathf.RoundToInt(_tower.towerStats.cost* 0.6f);
            GoldManager.Instance.AddGold(refund);
            AudioManager.Instance?.PlaySfx(5);
            UIController.Instance.HideUpgradeUI();
            Destroy(_tower.gameObject);
        }
    }
}
