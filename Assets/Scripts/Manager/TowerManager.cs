using UnityEngine;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private Tower activeTower;
    public static TowerManager instance;
    
    [SerializeField] private Transform indicator;
    [SerializeField] private LayerMask  Placement;
    public bool isPlacing;
    

    

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing)
        {
            indicator.position = GetGridPosition();

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                isPlacing = false;
                Instantiate(activeTower, indicator.position, activeTower.transform.rotation);

                indicator.gameObject.SetActive(false);
            }
        }
    }

    public void PlaceTheTower(Tower placeTower)
    {
        activeTower = placeTower;
        
        isPlacing = true;

        Destroy(indicator.gameObject);    
        Tower placedTower = Instantiate(activeTower);
        placedTower.enabled = false;
        indicator = placedTower.transform;

        Debug.Log("Plazier mich Hart, Du Sau!!!");
    }

    public Vector3 GetGridPosition()
    {
        Vector3 location = Vector3.zero;

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 200f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f, Placement))
        {
            location = hit.point;
        }

        location.y = 0f;
        
        return location;
    }
}
