using UnityEngine;

public class BallController : MonoBehaviour
{
    public float spawnSpeed = 5f;  
    public float moveSpeed = 20f;
    public GameObject objectPrefab;
    
    private Transform _originalTransform;
    private Transform _cloneTransform;
    private GameObject _clone;
    private Camera _mainCamera;

    private Vector3 _originalScale;
    private Vector3 _cloneScale;
    private bool _isTouching;
    private Rigidbody _rigidbody;
    private bool _isRigidbodyNotNull;
    private bool _isCloneNull;
    private bool _isMainCameraNull;

    private void Start()
    {
        _isMainCameraNull = _mainCamera == null;
        _isCloneNull = _clone == null;
        _mainCamera = Camera.main;
        _originalTransform = transform;

        _originalScale = _originalTransform.localScale;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase != TouchPhase.Began || _isMainCameraNull)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    _isTouching = false;
                    ShootingBall();
                }
            }
            else
            {
                if (_isCloneNull)
                {
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    var cloneRenderer = objectPrefab.GetComponent<Renderer>();
                    var cloneHeight = cloneRenderer.bounds.size.y;

                    var yPos = cloneHeight * 0.5f;

                    _clone = Instantiate(objectPrefab, new Vector3(0, yPos, 5f), Quaternion.identity);
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    _cloneTransform = _clone.GetComponent<Transform>();
                    _cloneScale = Vector3.zero;
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    _rigidbody = _clone.GetComponent<Rigidbody>();
                    // ReSharper disable once Unity.PerformanceCriticalCodeNullComparison
                    _isRigidbodyNotNull = _rigidbody != null;
                }

                _isTouching = true;
            }
        }

        if (!_isTouching) return;
        _cloneScale += new Vector3(spawnSpeed * Time.deltaTime, spawnSpeed * Time.deltaTime, spawnSpeed * Time.deltaTime);
        _cloneTransform.localScale = _cloneScale;

        _originalScale -= new Vector3(spawnSpeed * Time.deltaTime, spawnSpeed * Time.deltaTime, spawnSpeed * Time.deltaTime);
        _originalTransform.localScale = _originalScale;
    }

    private void ShootingBall()
    {
        if (_isRigidbodyNotNull)
        {
            _rigidbody.AddForce(0, 0, moveSpeed, ForceMode.Impulse);
        }
    }
}
