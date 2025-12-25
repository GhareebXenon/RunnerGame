using UnityEngine;

public class PlayerButtonInput : MonoBehaviour
{
    public KeyCode runKey = KeyCode.W;

    [Header("Speed Settings")]
    [Range(0f, 1f)]
    public float baseSpeed01 = 0.15f;
    public float boostPerPress = 0.15f;
    public float decaySpeed = 1.8f;

    float speed01;
    bool raceActive;

    void Start()
    {
        speed01 = 0f; // frozen until GO
    }

    void Update()
    {
        if (!raceActive) return;

        if (Input.GetKeyDown(runKey))
        {
            speed01 += boostPerPress;
        }

        speed01 -= decaySpeed * Time.deltaTime;
        speed01 = Mathf.Clamp(speed01, baseSpeed01, 1f);
    }

    public float GetSpeed01()
    {
        return raceActive ? speed01 : 0f;
    }

    public void SetRaceActive(bool active)
    {
        raceActive = active;

        if (active)
            speed01 = baseSpeed01; // start walking on GO
        else
            speed01 = 0f;
    }
}
