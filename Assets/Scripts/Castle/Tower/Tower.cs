using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [Header("Tower Stats")]
    [SerializeField] TowerStats towerBasicStats;

    [SerializeField] private LayerMask findTheEnemy;
    [SerializeField] private Collider[]  collidersInRange;
    [SerializeField] public List<Enemy>  enemiesInRange = new List<Enemy>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collidersInRange = Physics.OverlapSphere(transform.position, towerBasicStats.range, findTheEnemy);
        
        enemiesInRange.Clear();
        foreach (Collider col in collidersInRange)
        {
            enemiesInRange.Add(col.GetComponent<Enemy>());
        }
    }
}
