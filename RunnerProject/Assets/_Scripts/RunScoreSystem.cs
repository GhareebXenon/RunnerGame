using UnityEngine;

public class RunScoreSystem : MonoBehaviour
{
    public SpeedController speedController;

    public float scoreMultiplier = 10f;

    float score;
    float highScore;

    void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
    }

    void Update()
    {
        float speed = speedController.GetCurrentSpeed();
        score += speed * scoreMultiplier * Time.deltaTime;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
    }

    public float GetScore01(float maxScore)
    {
        return Mathf.Clamp01(score / maxScore);
    }

    public float GetHighScore()
    {
        return highScore;
    }
}
