using UnityEngine;

public class PlayerButtonInputEsp : MonoBehaviour
{
    public ESP32Receiver udp;

    [Header("Speed Settings")]
    public float speed02;
    public float boostPerStep = 0.12f;
    public float decayPerSecond = 0.6f;

    // Step queue
    private int pendingSteps = 0;

    void OnEnable()
    {
        if (udp != null)
            udp.OnStep.AddListener(HandleStep);
    }

    void OnDisable()
    {
        if (udp != null)
            udp.OnStep.RemoveListener(HandleStep);
    }

    void HandleStep(int foot)
    {
        // Increment pending steps when ESP32 sends a step
        pendingSteps++;
        Debug.Log($"Step received in PlayerButtonInputEsp - Foot: {(foot == 1 ? "Right" : "Left")}, PendingSteps: {pendingSteps}");
    }

    void Update()
    {
        // Apply boost for each pending step
        if (pendingSteps > 0)
        {
            speed02 += boostPerStep * pendingSteps;
            pendingSteps = 0; // reset after applying
        }

        // Decay speed
        speed02 -= decayPerSecond * Time.deltaTime;
        speed02 = Mathf.Clamp01(speed02);
    }

    public float GetSpeed02()
    {
        return speed02;
    }
}
