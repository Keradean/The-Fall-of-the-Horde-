using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rB;
    [SerializeField] private float firingSpeed;
	[HideInInspector] public float damage;
	[HideInInspector] public float burnDamage;
	[HideInInspector] public float burnDuration;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rB.linearVelocity = transform.forward *  firingSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
		{
		enemyHealth.TakeDamage(damage);
		
			//pls Burn you nasty Bitch
			if(burnDuration > 0)
			{
				other.GetComponent<Enemy>().SetOnFire(burnDamage, burnDuration);
			}
        Destroy(gameObject);
		}
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
