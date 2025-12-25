using UnityEngine;

public class RaceModeManager : MonoBehaviour
{
    public static RaceModeManager Instance;

    public bool carMode;

    public PlayerVisualController[] players;
    public GameObject roadEnvironment;

    void Awake()
    {
        Instance = this;
    }

    public void SetCarMode(bool enabled)
    {
        carMode = enabled;

        foreach (var p in players)
            p.SetMode(enabled);

        roadEnvironment.SetActive(enabled);
    }
}