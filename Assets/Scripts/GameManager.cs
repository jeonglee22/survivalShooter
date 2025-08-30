using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;

    private int score;

    public void AddScore(int s)
    {
        score += s;
        uiManager.SetScoreText(score);
    }
}
