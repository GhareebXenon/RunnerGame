using UnityEngine;
using TMPro;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    [Header("UI")]
    public TextMeshProUGUI winText;
    public GameObject playAgainButton;

    bool raceFinished;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        winText.gameObject.SetActive(false);
        playAgainButton.SetActive(false);
    }

    public void PlayerFinished(PlayerMovement winner)
    {
        if (raceFinished) return;

        raceFinished = true;

        // Stop all players
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
        foreach (var p in players)
            p.Stop();

        // Show winner UI
        winText.text = winner.gameObject.name + " Wins!";
        winText.gameObject.SetActive(true);

        playAgainButton.SetActive(true);
    }
}
