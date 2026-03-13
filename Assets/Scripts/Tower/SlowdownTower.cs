using UnityEngine;

public class SlowdownTower : MonoBehaviour
{
    [SerializeField] private float slowDownBitch;
 
    private Tower _tower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //Collider an Range anpassen
       GetComponent<SphereCollider>().radius = GetComponent<Tower>().towerStats.range;
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy)) enemy.speedMod = slowDownBitch;
    }   
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy)) enemy.speedMod = 1f;
    }
    
    
    
}
