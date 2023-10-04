using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public LayerMask obstacleLayer;
    public float raycastDistance = 2f;
    public float jumpInterval = 1f;
    public float jumpHeight = 1f;

    private bool _isJumping;
    private float _jumpTimer;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var castRadius = GetComponent<SphereCollider>().radius * 3.5f;
        var position = transform.position;
        var directionToTarget = target.position - position;
        directionToTarget.y = 0f;
        var distanceToTarget = directionToTarget.magnitude;

        var hasClearPath = !Physics.Raycast(position, directionToTarget.normalized, out _, raycastDistance, obstacleLayer);

        if (Physics.SphereCast(position, castRadius, directionToTarget.normalized, out _, distanceToTarget,
                obstacleLayer) || !hasClearPath) return;
        transform.Translate(directionToTarget.normalized * (moveSpeed * Time.deltaTime), Space.World);

        _jumpTimer += Time.deltaTime;

        if (_isJumping || !(_jumpTimer >= jumpInterval)) return;
        Jump();
        _jumpTimer = 0f;
    }

    private void Jump()
    {
        _isJumping = true;
        _rb.AddForce(Vector3.up * (jumpForce + jumpHeight), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }
}