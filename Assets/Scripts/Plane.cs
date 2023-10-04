using UnityEngine;

public class Plane : MonoBehaviour
{
    public GameObject ball;
    public GameObject cube;

    private Transform _transform;
    private Transform _ballPosition;
    private Transform _cubePosition;
    private Renderer _ballRenderer;
    private Vector3 _position;

    private void Start()
    {
        _ballPosition = ball.GetComponent<Transform>();
        _cubePosition = cube.GetComponent<Transform>();
        _transform = gameObject.GetComponent<Transform>();
        _ballRenderer = ball.GetComponent<Renderer>();
    }

    private void Update()
    {
        var cubePosition = PlanePosition(out var ballPosition);
        ResizePlane(cubePosition, ballPosition);
    }

    private void ResizePlane(Vector3 cubePosition, Vector3 ballPosition)
    {
        var distance = Vector3.Distance(cubePosition, ballPosition) / 10f;
        var newScale = _transform.localScale;
        var wight = _ballRenderer.bounds.size.x / 18f;
        newScale.z = distance;
        newScale.x = wight;
        _transform.localScale = newScale;
    }

    private Vector3 PlanePosition(out Vector3 ballPosition)
    {
        var cubePosition = _cubePosition.position;
        ballPosition = _ballPosition.position;
        _position = (cubePosition + ballPosition) / 2f;
        _transform.position = new Vector3(_position.x, 0.01f, _position.z);
        return cubePosition;
    }
}