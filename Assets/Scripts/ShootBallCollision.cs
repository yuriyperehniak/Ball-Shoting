using Unity.VisualScripting;
using UnityEngine;

public class ShootBallCollision : MonoBehaviour
{
    public float infectionMultiplier = 3f;
    public GameObject explosionPrefab;
    private Transform _shootBall;

    private void Start()
    {
        _shootBall = gameObject.GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionGameObject = collision.gameObject;

        if (collisionGameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            InfectionObstacles();    
        }
        else if (collisionGameObject.CompareTag("Cube"))
        {
            Destroy(gameObject);
        }
    }

    private void InfectionObstacles()
    {
        var ballScale = _shootBall.transform.localScale.x;
        var ballPosition = _shootBall.position;
        var infectionRadius = infectionMultiplier * ballScale;
        var infectionsObstacles = Physics.OverlapSphere(ballPosition, infectionRadius);

        foreach (var colliderInRadius in infectionsObstacles)
        {
            if (!colliderInRadius.CompareTag("Obstacle")) return;
            var infectionGameObject = colliderInRadius.GameObject();
            var infectionTransform = infectionGameObject.GetComponent<Transform>();
            Instantiate(explosionPrefab, infectionTransform.position, Quaternion.identity);
            Destroy(infectionGameObject);
        }
    }
}