using Extra;
using Tower.TowerStats;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Manager
{
	public class TowerManager : Singleton<TowerManager>
	{
		[SerializeField] private Tower.Tower activeTower;
		[SerializeField] private TowerStats towerStats;
    
		[SerializeField] private Transform indicator;
		[SerializeField] private LayerMask  groundLayer;
		[SerializeField] private LayerMask  castleLayer;
		[FormerlySerializedAs("AreThereObstacles")] [SerializeField] private LayerMask  areThereObstacles;
  
		public bool isPlacing;
		public bool canPlace = true;
		////////////////////////////////////////////////////////////////////////////////////////////////
		// Update is called once per frame
		private void Update()
		{
			PlacingTheTower();
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private void PlacingTheTower()
		{
			if (!isPlacing) return;
			{
				indicator.position = GetGridPosition();
				canPlace = true;
				RaycastHit hit; 
				if(Physics.Raycast(indicator.position + Vector3.down, Vector3.up, out hit, 10f, areThereObstacles))
				{
					canPlace = false;
				}
				indicator.gameObject.SetActive(true);
				var rend = indicator.GetComponentInChildren<Renderer>();
				if(rend != null)
				{
					rend.material.color = canPlace ? Color.green : Color.red;
				}

				if (!Mouse.current.leftButton.wasPressedThisFrame || !canPlace) return;
				if (!GoldManager.Instance.SpendGold(towerStats.cost)) return;
				isPlacing = false;
				Instantiate(activeTower, indicator.position, activeTower.transform.rotation);
				indicator.gameObject.SetActive(false);
			}
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		public void PlaceTheTower(Tower.Tower placeTower)
		{
			activeTower = placeTower;
			towerStats = placeTower.UpgradeLevelOne;
			isPlacing = true;

			Destroy(indicator.gameObject);    
			var placedTower = Instantiate(activeTower);
			placedTower.enabled = false;

			foreach (var col in placedTower.GetComponentsInChildren<Collider>()) col.enabled = false;
			indicator = placedTower.transform;

			placedTower.rangeIndicator.SetActive(true);
			
			var range = placeTower.UpgradeLevelOne.range *2f;
			placedTower.rangeIndicator.transform.localScale = new Vector3(range, 0.1f, range);
		}   
		////////////////////////////////////////////////////////////////////////////////////////////////v
		public void DontPlaceTheTower()
		{
			if (!isPlacing) return;
			isPlacing = false;
			Debug.Log("Plazier mich nicht Hart, Du Sau!!!");

			if(indicator != null)
			{
				indicator.gameObject.SetActive(false);
			}
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		private Vector3 GetGridPosition()
		{
			var location = indicator.position;

			var mousePosition = Mouse.current.position.ReadValue();

			if (Camera.main == null) return location;
			var ray = Camera.main.ScreenPointToRay(mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 200f, Color.red);

			{
				// CastleTower ..
				RaycastHit hit;
				if(activeTower.isCastleTower)
				{
					if (!Physics.Raycast(ray, out hit, 200f, castleLayer)) return location;
					location = hit.point;
					location.y = hit.point.y;

				}
				// i call it here Ground Tower (normal tower)
				else
				{
					if (!Physics.Raycast(ray, out hit, 200f, groundLayer)) return location;
					location = hit.point;
					location.y = hit.point.y;
				}
			}
			return location;
  
		}
	}
}
