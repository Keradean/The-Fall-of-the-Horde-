using UnityEngine;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private Tower activeTower;
    [SerializeField] private TowerStats towerStats;
    public static TowerManager instance;
    
    [SerializeField] private Transform indicator;
    [SerializeField] private LayerMask  ICanOnlyPlaceItThere;
    [SerializeField] private LayerMask  AreThereObstacles;
  
	public bool isPlacing;

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
		if (isPlacing)
        {
            indicator.position = GetGridPosition();

			RaycastHit hit; 
			if(Physics.Raycast(indicator.position + new Vector3(0f, -2, 0f), Vector3.up, out hit, 10f, AreThereObstacles))
			{
				indicator.gameObject.SetActive(false);
			}
			else
			{
				indicator.gameObject.SetActive(true);
			}

            if (Mouse.current.leftButton.wasPressedThisFrame && indicator.gameObject.activeSelf)
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
        Vector3 location = Vector3.zero;

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 200f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f, ICanOnlyPlaceItThere))
        {
            location = hit.point;
        }

        location.y = 0f;
        
        return location;
    }
}
