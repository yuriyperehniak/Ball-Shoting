using System;
using Unity.VisualScripting;
using UnityEngine;

public class CloneBallCollision : MonoBehaviour
{
    public float infectionMultiplier = 3f;
    private Transform _shootBall;

    private void Start()
    {
        _shootBall = gameObject.GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionGameObject = collision.gameObject;

        if (!collisionGameObject.CompareTag("Obstacle")) return;

        InfectionObstacles();
        
        Destroy(gameObject);
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
            var destroyGameObject= colliderInRadius.GameObject();
            Destroy(destroyGameObject);
        }
    }
}