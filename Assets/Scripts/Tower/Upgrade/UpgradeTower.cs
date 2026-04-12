using Manager;
using UnityEngine;

namespace Tower.Upgrade
{
    public class UpgradeTower : MonoBehaviour
    {
        private Tower _tower;
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnUpgradeButton()
        {
            if (GoldManager.Instance.SpendGold(_tower.towerStats.upgradeCost)) _tower.Upgrade();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnSell()
        {
            var refund = Mathf.RoundToInt(_tower.towerStats.cost* 0.6f);
            GoldManager.Instance.AddGold(refund);
            Destroy(_tower.gameObject);
        }
    }
}
