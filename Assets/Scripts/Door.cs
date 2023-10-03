using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject ball;
    public GameObject rotationDoorCube;
    public float openDistance = 5f;
    public float rotationSpeed = 30f;

    private Transform _doorTransform;
    private Transform _rotationDoorCubeTransform;
    private Vector3 _initialPosition;
    private bool _isOpen;

    private void Start()
    {
        _doorTransform = transform;
        _rotationDoorCubeTransform = rotationDoorCube.transform;
        _initialPosition = _doorTransform.position;
    }

    private void Update()
    {
        var distance = Vector3.Distance(_doorTransform.position, ball.transform.position);

        _isOpen = distance < openDistance * 10f;
        if (_isOpen)
        {
            var step = rotationSpeed * Time.deltaTime;
            var targetRotation = Quaternion.Euler(0f, -45f, 0f);

            _rotationDoorCubeTransform.rotation =
                Quaternion.RotateTowards(_rotationDoorCubeTransform.rotation, targetRotation, step);

            _doorTransform.position =
                Vector3.MoveTowards(_doorTransform.position, _initialPosition + new Vector3(0f, 0f, -1f), step);
        }
        else
        {
            _rotationDoorCubeTransform.rotation = Quaternion.Euler(0f, -180f, 0f);
            _doorTransform.position = _initialPosition;
        }
    }
}