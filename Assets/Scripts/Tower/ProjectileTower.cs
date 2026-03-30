using UnityEngine;

public class ProjectileTower : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform  firePoint;
    [SerializeField] private Transform  towerWeapon;

    
    private Tower _tower;
    private float _shotCounter;
    private Transform _target;
	private ProjectileStats _projectileStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tower = GetComponent<Tower>();
		_projectileStats = _tower.towerStats as ProjectileStats;
		Debug.Log("towerStats: " + _tower.towerStats);
    	Debug.Log("_projectileStats: " + _projectileStats);
		
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            towerWeapon.LookAt(_target);
            // ToDo - schau dir Slerp an *  | | |||| *
            //towerWeapon.rotation = Quaternion.LookRotation(target.position - transform.position);
			towerWeapon.rotation = Quaternion.Euler(0f, towerWeapon.rotation.eulerAngles.y, 0f );// die Waffe des Towers dreht sich nicht mehr nach unten, sollte sie höher stehen
        }
        _shotCounter -= Time.deltaTime;
        if (_shotCounter <= 0 && _target != null)
        {
            _shotCounter = _projectileStats.timeBetweenAttacks;
            
            firePoint.LookAt(_target);

            GameObject pt = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
			Projectile projectile = pt.GetComponent<Projectile>();
			projectile.damage = _projectileStats.damage;
		//if is a Fire Tower
		if(_projectileStats is FireTowerStats fireStats)
		{
			projectile.burnDamage = fireStats.burnDamage;
			projectile.burnDuration = fireStats.burnDuration;
		}
        }

        if (_tower.enemiesInRange.Count > 0)
        {
            float minDistance = _projectileStats.range + 1f;
            foreach (Enemy enemy in _tower.enemiesInRange)
            {
                if (enemy != null)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        _target = enemy.transform;
                    }
                }
            }
        }
        else
        {
            _target = null;
        }
    }
}
