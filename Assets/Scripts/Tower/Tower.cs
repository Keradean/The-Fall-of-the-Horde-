using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tower
{
	public class Tower : MonoBehaviour, IPointerClickHandler
	{
		[Header("Tower Stats")]
		public TowerStats.TowerStats towerStats;

		[Header("Upgrade")]
		[SerializeField] private TowerStats.TowerStats[] upgradeLevel;
		private int _currentLevel;
		public GameObject towerPrefab;

		[Header("Tower Range Indicator")]
		[SerializeField] public GameObject rangeIndicator;

		[SerializeField] private LayerMask findTheEnemy;
		[SerializeField] private Collider[]  collidersInRange;
		public List<Enemy.Enemy>  enemiesInRange = new();

		public bool isCastleTower;
		////////////////////////////////////////////////////////////////////////////////////////////////
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		private void Start()
		{
			towerStats = Instantiate(upgradeLevel[0]);   
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		// Update is called once per frame
		private void Update()
		{
			collidersInRange = Physics.OverlapSphere(transform.position, towerStats.range, findTheEnemy);
			enemiesInRange.Clear();
			foreach (Collider col in collidersInRange)
				if(col.TryGetComponent(out Enemy.Enemy enemy))enemiesInRange.Add(enemy);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		// Upgrade your Tower
		public void Upgrade()
		{
			if (_currentLevel >= upgradeLevel.Length - 1) return; // verhindere das du über das letzte level hinaus gehst
			_currentLevel ++;
			towerStats = Instantiate(upgradeLevel[_currentLevel]); // Tausche das ScriptableObject aus
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void OnPointerClick(PointerEventData eventData)
		{
			UIController.Instance.ShowUpgradeUI(this);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
	}
}
