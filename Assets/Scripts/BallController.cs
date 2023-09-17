using UnityEngine;

public class BallController : MonoBehaviour
{
    public float spawnSpeed = 10f;
    public float moveSpeed = 70f;
    public GameObject objectPrefab;

    private Transform _sizeOriginal;
    private Transform _sizeClone;
    private GameObject _clone;

    private bool _isTouching;
    private Transform _transform;
    private Camera _camera;
    private bool _isCameraNotNull;
    private Transform _transform1;
    private bool _isCloneNotNull;

    private void Start()
    {
        _isCloneNotNull = _clone != null;
        _transform1 = _clone.GetComponent<Transform>();
        var main = Camera.main;
        _isCameraNotNull = main != null;
        _camera = main;
        _transform = gameObject.GetComponent<Transform>();
        _sizeOriginal = _transform;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                if (touch.phase != TouchPhase.Began) continue;
                if (_isTouching || !_isCameraNotNull) continue;
                var worldPosition = _camera.ScreenToWorldPoint(touch.position);

                if (_isCloneNotNull) continue;
                _clone = Instantiate(objectPrefab, worldPosition, Quaternion.identity);
                _sizeClone = _transform1;
                _isTouching = true;
            }
        }

        if (!_isTouching) return;
        var growthRate = Time.deltaTime * spawnSpeed;
        _sizeClone.localScale += new Vector3(growthRate, growthRate, growthRate);
        _sizeOriginal.localScale -= new Vector3(growthRate, growthRate, growthRate);
    }

    public void OnTouchUp()
    {
        _isTouching = false;
    }
}