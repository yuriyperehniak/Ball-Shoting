using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject winGamePanel;
    public GameObject ball;

    private bool _winningConditions;
    private BallController _ballController;

    private void Start()
    {
        _ballController = ball.GetComponent<BallController>();
    }

    private void GameOverAction()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }


    public void WinGameActions()
    {
        winGamePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void MinimalCriticalSize()
    {
        var minimalScale = _ballController.originalStartScale.x / 5;
        var scale = _ballController.originalScale.x;
        if (scale < minimalScale)
        {
            GameOverAction();
        }
    }
 }