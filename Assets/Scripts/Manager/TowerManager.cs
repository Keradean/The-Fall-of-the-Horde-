using UnityEngine;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private Tower activeTower;
    [SerializeField] private TowerStats towerStats;
    public static TowerManager instance;
    
    [SerializeField] private Transform indicator;
    [SerializeField] private LayerMask  groundLayer;
    [SerializeField] private LayerMask  castleLayer;
    [SerializeField] private LayerMask  AreThereObstacles;
  
	public bool isPlacing;
	public bool canPlace = true;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
		PlacingTheTower();
    }

	public void PlacingTheTower()
	{
		if (!isPlacing) return;
        {
            indicator.position = GetGridPosition();
			canPlace = true;

			RaycastHit hit; 
			if(Physics.Raycast(indicator.position + Vector3.down, Vector3.up, out hit, 10f, AreThereObstacles))
			{
				canPlace = false;
			}
			indicator.gameObject.SetActive(true);

			Renderer rend = indicator.GetComponentInChildren<Renderer>();
			if(rend != null)
			{
				rend.material.color = canPlace ? Color.green : Color.red;
			}

            if (Mouse.current.leftButton.wasPressedThisFrame && canPlace)
            {
				if(GoldManager.instance.SpendGold(towerStats.cost))
				{
				isPlacing = false;
                Instantiate(activeTower, indicator.position, activeTower.transform.rotation);

                indicator.gameObject.SetActive(false);
				}

            }
        }
	}

    public void PlaceTheTower(Tower placeTower)
    {
        activeTower = placeTower;
		towerStats = placeTower.towerStats;
        isPlacing = true;

        Destroy(indicator.gameObject);    
        Tower placedTower = Instantiate(activeTower);
        placedTower.enabled = false;

		foreach (Collider col in placedTower.GetComponentsInChildren<Collider>()) col.enabled = false;
        indicator = placedTower.transform;

		placedTower.rangeIndicator.SetActive(true);
		placedTower.rangeIndicator.transform.localScale = new Vector3(towerStats.range, 0.001f, towerStats.range );
		

        Debug.Log("Plazier mich Hart, Du Sau!!!");
    }   
	
	public void DontPlaceTheTower()
    {
        if(isPlacing)
		{
        isPlacing = false;
        Debug.Log("Plazier mich nicht Hart, Du Sau!!!");

		if(indicator != null)
		{
			indicator.gameObject.SetActive(false);
		}
	}    
}

    public Vector3 GetGridPosition()
    {

			Vector3 location = indicator.position;

       	 	Vector2 mousePosition = Mouse.current.position.ReadValue();
        
        	Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        	Debug.DrawRay(ray.origin, ray.direction * 200f, Color.red);

        	RaycastHit hit;
        	
        	{
				// CastleTower ..
                if(activeTower.isCastleTower)
				{
					if (Physics.Raycast(ray, out hit, 200f, castleLayer))
					{
						location = hit.point;
						location.y = 0.8f;
					}

				}
				// i call it here Ground Tower (normal tower)
				else
				{
					if (Physics.Raycast(ray, out hit, 200f, groundLayer))
					{
						location = hit.point;
						location.y = 0f;
					}
				}
            	
        	}
        
       		 return location;
  
    }
}
