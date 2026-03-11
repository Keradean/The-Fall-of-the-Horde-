using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Transform gameObject;
    [SerializeField] private float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        gameObject.Rotate(0f, rotationSpeed, 0f);
    }
}
