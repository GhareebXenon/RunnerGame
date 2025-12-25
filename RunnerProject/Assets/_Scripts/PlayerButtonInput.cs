using UnityEngine;

public class PlayerButtonInput : MonoBehaviour
{
    public KeyCode runKey = KeyCode.W;

    [Header("Speed Settings")]
    [Range(0f, 1f)]
    public float baseSpeed01 = 0.15f; // walking speed

    public float boostPerPress = 0.15f;
    public float decaySpeed = 1.8f;

    float speed01;

    void Start()
    {
        speed01 = baseSpeed01; // start walking
    }

    void Update()
    {
        if (Input.GetKeyDown(runKey))
        {
            speed01 += boostPerPress;
        }

        speed01 -= decaySpeed * Time.deltaTime;

        // Never go below walking speed
        speed01 = Mathf.Clamp(speed01, baseSpeed01, 1f);
    }

    public float GetSpeed01()
    {
        return speed01;
    }
}
