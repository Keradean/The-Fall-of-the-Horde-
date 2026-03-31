using Manager;
using Projectile;
using Tower.TowerStats;
using UnityEngine;

namespace Tower
{
    public class ProjectileTower : MonoBehaviour
    {
        [SerializeField] private Transform  firePoint;
        [SerializeField] private Transform  towerWeapon;
        
        private Tower _tower;
        private float _shotCounter;
        private Transform _target;
        private ProjectileStats _projectileStats;
        ////////////////////////////////////////////////////////////////////////////////////////////////
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _tower = GetComponent<Tower>();
            _projectileStats = _tower.towerStats as ProjectileStats;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        // Update is called once per frame
        private void Update()
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
                //ToDo
                // je nach Tower Typ anpassen ?fireTower :? iceTower : arrowtower 
                var projectile = PoolManager.Instance.ArrowPool.Get();
                projectile.transform.position = firePoint.position;
                projectile.transform.rotation = firePoint.rotation;
                projectile.damage = _projectileStats.damage;
                projectile.LaunchProjectile();
                //if is a Fire Tower
                if(_projectileStats is FireTowerStats fireStats)
                {
                    projectile.burnDamage = fireStats.burnDamage;
                    projectile.burnDuration = fireStats.burnDuration;
                }
            }

            if (_tower.enemiesInRange.Count > 0)
            {
                var minDistance = _projectileStats.range + 1f;
                foreach (var enemy in _tower.enemiesInRange)
                {
                    if (enemy == null) continue;
                    var distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (!(distance < minDistance)) continue;
                    minDistance = distance;
                    _target = enemy.transform;
                }
            }
            else
            {
                _target = null;
            }
        }
    }
}
