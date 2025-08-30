using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;

    private int score;
	private float gameOverTime;

    public bool IsGameOver { get; set; }
	private float gameOverUIFadingTime = 4f;

	private void Awake()
	{
        IsGameOver = false;
	}

	public void AddScore(int s)
    {
        score += s;
        uiManager.SetScoreText(score);
    }

	private void Update()
	{
		if(IsGameOver)
		{
			if(Time.unscaledTime - gameOverTime > gameOverUIFadingTime)
			{
				gameOverTime = Time.unscaledTime;
				IsGameOver = false;
				uiManager.SetGameOverUI(false);
				Time.timeScale = 1f;
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
		else
			gameOverTime = Time.unscaledTime;
	}
}
