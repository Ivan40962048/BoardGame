using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _cameraMovementSpeed = 2f;
    [SerializeField] private float _sizeOfSensitiveEdges = 0;
    private void Update()
    {
        var positionDelta = _cameraMovementSpeed * Time.deltaTime;
        if (Input.mousePosition.x <= _sizeOfSensitiveEdges)
            transform.position += NullYCoordinate(-Camera.main.transform.right * positionDelta);
        if (Input.mousePosition.y <= _sizeOfSensitiveEdges)
            transform.position += NullYCoordinate(-(Camera.main.transform.forward + Camera.main.transform.up) * positionDelta);
        if (Input.mousePosition.x >= Screen.width - (1 + _sizeOfSensitiveEdges))
            transform.position += NullYCoordinate(Camera.main.transform.right * positionDelta);
        if (Input.mousePosition.y >= Screen.height - (1 + _sizeOfSensitiveEdges))
            transform.position += NullYCoordinate((Camera.main.transform.forward + Camera.main.transform.up) * positionDelta);
    }

    private Vector3 NullYCoordinate(Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }
}
