using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileStats _projectileStats;
    [SerializeField] private Rigidbody rB;

    [SerializeField] private float firingSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rB.linearVelocity = transform.forward *  firingSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
		{
		enemyHealth.TakeDamage(_projectileStats.damage);
        Destroy(gameObject);
		}
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
