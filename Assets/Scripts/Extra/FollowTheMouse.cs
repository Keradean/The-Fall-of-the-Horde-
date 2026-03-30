using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Extra
{
    public class FollowTheMouse : MonoBehaviour
    {
        [FormerlySerializedAs("_speed")] [SerializeField] private float speed;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            FollowMouse();
        }

        private void FollowMouse()
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime);
                transform.LookAt(hit.point);
            }
        }
    }
}
