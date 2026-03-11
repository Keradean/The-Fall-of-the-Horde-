using UnityEngine;
using UnityEngine.InputSystem;

public class FollowTheMouse : MonoBehaviour
{
    [SerializeField] private float _speed;
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
            transform.position = Vector3.MoveTowards(transform.position, hit.point, _speed * Time.deltaTime);
            transform.LookAt(hit.point);
        }
    }
}
