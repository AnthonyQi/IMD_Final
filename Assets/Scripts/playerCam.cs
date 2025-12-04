using UnityEngine;
using UnityEngine.InputSystem;

public class playerCam : MonoBehaviour
{
    public float xSensitivity, ySensitivity;
    public Transform orientation;
    float xRotation, yRotation;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * Time.deltaTime;

        float mouseX = mouseDelta.x * xSensitivity;
        float mouseY = mouseDelta.y * ySensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
