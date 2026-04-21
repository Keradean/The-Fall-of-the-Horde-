using Manager;
using Tower.TowerStats;
using UnityEngine;

namespace Tower
{
	public class SlowdownTower : MonoBehaviour
	{
 
		private Tower _tower;
		private SlowDownTowerStats _slowStats;
		////////////////////////////////////////////////////////////////////////////////////////////////
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		void Start()
		{
			_tower = GetComponent<Tower>();
			_slowStats = _tower.towerStats as SlowDownTowerStats;
			//Collider an Range anpassen
			if (_slowStats != null) GetComponent<SphereCollider>().radius = _slowStats.range;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Enemy.Enemy>(out Enemy.Enemy enemy)) enemy.speedMod = _slowStats.slowDownAmount;
			AudioManager.Instance.PlaySfx(4);
		}   
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent<Enemy.Enemy>(out Enemy.Enemy enemy)) enemy.speedMod = 1f;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void Upgrade()
		{
			_tower.Upgrade();
			_slowStats = _tower.towerStats as SlowDownTowerStats; // get the new Stats
			if (_slowStats != null) GetComponent<SphereCollider>().radius = _slowStats.range;
		}
	}
}
