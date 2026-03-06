using UnityEngine;

public class ProjectileTower : MonoBehaviour
{
    [SerializeField] private TowerStats towerStats;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform  firePoint;
    [SerializeField] private Transform  towerWeapon;
    
    private Tower _tower;
    private float shotCounter;
    private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tower = FindFirstObjectByType<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            towerWeapon.LookAt(target);
            // ToDo - schau dir Slerp an *  | | |||| *
            //towerWeapon.rotation = Quaternion.LookRotation(target.position - transform.position);
			towerWeapon.rotation = Quaternion.Euler(0f, towerWeapon.rotation.eulerAngles.y, 0f );// die Waffe des Towers dreht sich nicht mehr nach unten, sollte sie höher stehen
        }
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0 && target != null)
        {
            shotCounter = towerStats.timeBetweenAttacks;
            
            firePoint.LookAt(target);

            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }

        if (_tower.enemiesInRange.Count > 0)
        {
            float minDistance = towerStats.range + 1f;
            foreach (Enemy enemy in _tower.enemiesInRange)
            {
                if (enemy != null)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = enemy.transform;
                    }
                }
            }
        }
        else
        {
            target = null;
        }
    }
}
