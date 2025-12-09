using UnityEngine;

public class cameramovement : MonoBehaviour
{
    public Transform target; // your player
    public Vector3 offset = new Vector3(0, 5, -7); // adjust as needed
    public float followSpeed = 5f;
    public float zoomSpeed = 2f;
public float minDistance = 2f;
public float maxDistance = 10f;
    
void LateUpdate()
{
    if (target == null) return;

    // Zoom control
    float scroll = Input.GetAxis("Mouse ScrollWheel");
    offset.z = Mathf.Clamp(offset.z + scroll * zoomSpeed, -maxDistance, -minDistance);

    Vector3 desiredPosition = target.position + offset;
    transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    transform.LookAt(target);
}
}
