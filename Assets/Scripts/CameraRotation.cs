using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _cameraRotationSpeed = 2.0f;

    private float yaw;
    private float pitch;

    private void Start()
    {
        pitch = transform.eulerAngles.x;
        yaw = transform.eulerAngles.y;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            yaw += _cameraRotationSpeed * Input.GetAxis("Mouse X");
            pitch -= _cameraRotationSpeed * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}