using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class RaceCountdownUI : MonoBehaviour
{
    public float countdownTime = 3f;

    [Header("UI")]
    public TextMeshProUGUI countdownText;

    [Header("Players")]
    public PlayerButtonInput[] players;

    [Header("Animation")]
    public float popScale = 1.5f;
    public float popDuration = 0.35f;

    Color yellow = new Color(1f, 0.85f, 0.2f);
    Color orange = new Color(1f, 0.5f, 0.1f);
    Color red = new Color(1f, 0.2f, 0.2f);
    Color green = new Color(0.2f, 1f, 0.3f);

    void Start()
    {
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        // Lock players
        foreach (var p in players)
            p.SetRaceActive(false);

        countdownText.gameObject.SetActive(true);

        yield return PlayNumber("3", yellow);
        yield return PlayNumber("2", orange);
        yield return PlayNumber("1", red);

        // GO!
        countdownText.text = "GO!";
        countdownText.color = green;

        countdownText.transform.localScale = Vector3.zero;

        countdownText.transform
            .DOScale(popScale * 1.2f, popDuration)
            .SetEase(Ease.OutBack);

        foreach (var p in players)
            p.SetRaceActive(true);

        yield return new WaitForSeconds(0.5f);

        // Fade out
        countdownText.DOFade(0f, 0.3f);
    }

    IEnumerator PlayNumber(string number, Color color)
    {
        countdownText.text = number;
        countdownText.color = color;
        countdownText.alpha = 1f;

        countdownText.transform.localScale = Vector3.zero;

        countdownText.transform
            .DOScale(popScale, popDuration)
            .SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(1f);
    }
}
