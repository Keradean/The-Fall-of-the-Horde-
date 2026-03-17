using UnityEngine;

public class SlowdownTower : MonoBehaviour
{
 
    private Tower _tower;
	private SlowDownTowerStats _slowStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		_tower = GetComponent<Tower>();
		_slowStats = _tower.towerStats as SlowDownTowerStats;
       //Collider an Range anpassen
       GetComponent<SphereCollider>().radius = _slowStats.range;
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy)) enemy.speedMod = _slowStats.slowDownAmount;
    }   
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy)) enemy.speedMod = 1f;
    }
    
    public void Upgrade()
	{
		_tower.Upgrade();
		_slowStats = _tower.towerStats as SlowDownTowerStats; // get the new Stats
		GetComponent<SphereCollider>().radius = _slowStats.range;
	}
}
