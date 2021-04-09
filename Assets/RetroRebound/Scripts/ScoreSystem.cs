using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    private int Score;
    private Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }
       
    public void IncrementScore(int points)
    {
        Score += points;
        scoreText.text = Score.ToString();
    }
    
    public void ResetScore()
    {
        Score = 0;
        scoreText.text = Score.ToString();
    }
}
