using UnityEngine;
using UnityEngine.InputSystem;

public class camMovement : MonoBehaviour
{
    public Transform cameraPos;
    void Update()
    {
        transform.position = cameraPos.position;
    }
}
