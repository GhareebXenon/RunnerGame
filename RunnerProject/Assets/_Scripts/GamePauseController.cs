using System.Collections.Generic;
using UnityEngine;

public class GamePauseController : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public GameObject StartPanel;
    void Awake()
    {
        // Pause the game at the start
        Time.timeScale = 0f;
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(false);
        }
    }

    public void PlayGame()
    {
        // Resume the game
        Time.timeScale = 1f;
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(true);
        }
        StartPanel.SetActive(false);
    }
}
