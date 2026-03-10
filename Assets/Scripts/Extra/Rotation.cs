using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Transform gameObject;
    [SerializeField] private float rotationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.Rotate(0f, rotationSpeed, 0f);
    }
}
