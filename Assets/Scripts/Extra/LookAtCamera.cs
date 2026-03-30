using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Die Healthbar soll immer richtung Camera schauen
        transform.eulerAngles = new Vector3(0, _mainCamera.transform.eulerAngles.y - 90,0);
    }
}
