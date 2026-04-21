using System.Collections;
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

		public TowerStats.TowerStats UpgradeLevelOne => upgradeLevel[0];
		[Header("Upgrade")]
		[SerializeField] private TowerStats.TowerStats[] upgradeLevel;
		private int _currentLevel;
		public GameObject towerPrefab;

		[Header("Tower Range Indicator")]
		[SerializeField] public GameObject rangeIndicator;

		[SerializeField] private LayerMask findTheEnemy;
		[SerializeField] private int maxCollidersInRange = 20; 
		private Collider[]  _collidersInRange;
		public List<Enemy.Enemy>  enemiesInRange = new();
		public bool attackFlying;
		public bool isCastleTower;
		public bool CanUpgrade => _currentLevel < upgradeLevel.Length - 1;
		////////////////////////////////////////////////////////////////////////////////////////////////
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		private void Awake()
		{
			_collidersInRange = new Collider[maxCollidersInRange];
		}		
		////////////////////////////////////////////////////////////////////////////////////////////////
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		private void Start()
		{
			towerStats = Instantiate(upgradeLevel[0]);
			StartCoroutine(CheckEnemiesInRange());
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private IEnumerator CheckEnemiesInRange()
		{
			while (true)
			{
				var hits = Physics.OverlapSphereNonAlloc(transform.position, towerStats.range, _collidersInRange,findTheEnemy); // Wie viele Einträge in diesem Array sind gültig
				enemiesInRange.Clear();
				for (var i = 0; i < hits; i++)
				{
					if(_collidersInRange[i].TryGetComponent(out Enemy.Enemy enemy))
					{
						if (enemy.isFlying && !attackFlying) continue;
						enemiesInRange.Add(enemy);
					}
				}
				yield return new WaitForSeconds(0.2f);
			}
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
