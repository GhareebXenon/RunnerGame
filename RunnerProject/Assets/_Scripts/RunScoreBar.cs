using UnityEngine;
using UnityEngine.UI;

public class RunScoreBar : MonoBehaviour
{
    public RunScoreSystem scoreSystem;
    public Image fillImage;
    public float maxScore = 100f;

    void Update()
    {
        fillImage.fillAmount = scoreSystem.GetScore01(maxScore);
    }
}