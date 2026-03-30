using UnityEngine;

namespace Extra
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }
}
