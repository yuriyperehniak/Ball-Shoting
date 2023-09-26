using UnityEngine;

public class BallController : MonoBehaviour
{
    public float spawnSpeed = 10f;
    public float moveSpeed = 30f;
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

    private void Start()
    {
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

            if (touch.phase != TouchPhase.Began || _mainCamera == null)
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
                    SpawnBallClone();
                }

                _isTouching = true;
            }
        }

        if (!_isTouching) return;
        ResizeBall();
    }
    
    private void SpawnBallClone()
    {
        var cloneRenderer = objectPrefab.GetComponent<Renderer>();
        var cloneHeight = cloneRenderer.bounds.size.y;

        var yPos = cloneHeight * 0.5f;
        _clone = Instantiate(objectPrefab, new Vector3(0f, yPos, 7f), Quaternion.identity);

        _cloneTransform = _clone.GetComponent<Transform>();
        _cloneScale = Vector3.zero;
        _rigidbody = _clone.GetComponent<Rigidbody>();
        _isRigidbodyNotNull = _rigidbody != null;
    }

    private void ResizeBall()
    {
        _cloneScale += new Vector3(spawnSpeed * Time.deltaTime, spawnSpeed * Time.deltaTime,
            spawnSpeed * Time.deltaTime);
        _cloneTransform.localScale = _cloneScale;

        _originalScale -= new Vector3(spawnSpeed * Time.deltaTime, spawnSpeed * Time.deltaTime,
            spawnSpeed * Time.deltaTime);
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