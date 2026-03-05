using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rB;

    [SerializeField] private float firingSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rB.linearVelocity = transform.forward *  firingSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
