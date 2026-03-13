using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [Header("Tower Stats")]
    public TowerStats towerStats;

    [SerializeField] private LayerMask findTheEnemy;
    [SerializeField] private Collider[]  collidersInRange;
	 public List<Enemy>  enemiesInRange = new List<Enemy>();
    
    [Header("Tower Range Indicator")]
    [SerializeField] public GameObject rangeIndicator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
        collidersInRange = Physics.OverlapSphere(transform.position, towerStats.range, findTheEnemy);
        
        enemiesInRange.Clear();
        foreach (Collider col in collidersInRange)
        {
            if(col.TryGetComponent<Enemy>(out Enemy enemy))enemiesInRange.Add(enemy);
        }
    }
}
