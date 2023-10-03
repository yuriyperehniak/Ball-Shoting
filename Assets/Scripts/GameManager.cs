using UnityEngine;

public class GameManager
{
    private readonly BallController _ballController;
    public GameObject gameOverPanel;

    public GameManager(BallController ballController)
    {
        _ballController = ballController;
    }

    public void MinimalCriticalSize()
    {
        var minimalScale = _ballController.originalStartScale.x/5;
        var scale = _ballController.originalScale.x;
        if (scale < minimalScale)
        {
            GameOverAction();
        }
    }

    private void GameOverAction()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}