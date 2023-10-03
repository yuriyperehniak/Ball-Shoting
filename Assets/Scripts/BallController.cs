using UnityEngine;
using UnityEngine.Serialization;

public class BallController : MonoBehaviour
{
    public float spawnSpeed = 15f;
    public float decreaseSpeed = 5f;
    public float moveSpeed = 30f;
    public GameObject objectPrefab;

    private Transform _originalTransform;
    private Transform _shootingBallTransform;
    private GameObject _shootingBall;
    private Camera _mainCamera;

    private Vector3 _directionToMove;
    public Vector3 originalScale;
    public Vector3 originalStartScale;
    private Vector3 _shootingBallScale;
    private Rigidbody _rigidbody;
    private RaycastHit _hit;
    private bool _isTouching;
    private bool _isRigidbodyNotNull;
    private bool _isShootingBallNull;
    private bool _hasReachedTarget;
    private readonly GameManager _gameManager;

    public BallController()
    {
        _gameManager = new GameManager(this);
    }

    private void Start()
    {
        Time.timeScale = 1;
        _isShootingBallNull = _shootingBall == null;
        _mainCamera = Camera.main;
        _originalTransform = transform;

        originalScale = _originalTransform.localScale;
        originalStartScale = originalScale;
     }

    private void Update()
    {
        _gameManager.MinimalCriticalSize();

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
                if (_isShootingBallNull)
                {
                    SpawnShootingBall();
                }

                _isTouching = true;
            }
        }

        if (!_isTouching) return;
        ResizeBall();
    }

    private void SpawnShootingBall()
    {
        var shootingBallRenderer = objectPrefab.GetComponent<Renderer>();
        var shootingBallHeight = shootingBallRenderer.bounds.size.y;

        var yPos = shootingBallHeight * 0.5f;
        _shootingBall = Instantiate(objectPrefab, new Vector3(0f, yPos, 7f), Quaternion.identity);

        _shootingBallTransform = _shootingBall.GetComponent<Transform>();
        _shootingBallScale = Vector3.zero;
        _rigidbody = _shootingBall.GetComponent<Rigidbody>();
        _isRigidbodyNotNull = _rigidbody != null;
    }

    private void ResizeBall()
    {
        _shootingBallScale += new Vector3(spawnSpeed * Time.deltaTime, spawnSpeed * Time.deltaTime,
            spawnSpeed * Time.deltaTime);
        _shootingBallTransform.localScale = _shootingBallScale;

        originalScale -= new Vector3(decreaseSpeed * Time.deltaTime, decreaseSpeed * Time.deltaTime,
            decreaseSpeed * Time.deltaTime);
        _originalTransform.localScale = originalScale;
    }

    private void ShootingBall()
    {
        if (_isRigidbodyNotNull)
        {
            _rigidbody.AddForce(0, 0, moveSpeed, ForceMode.Impulse);
        }
    }
}