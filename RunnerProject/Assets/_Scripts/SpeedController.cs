using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;

    [Header("TEST INPUT (Editor Only)")]
    [Range(0f, 1f)]
    public float testSpeed01; // Inspector slider

    [Header("Speed Settings")]
    public float maxSpeedValue = 2f;
    public float acceleration = 2f;
    public float deceleration = 3f;

    float currentSpeed;
    float targetSpeed;

    void Update()
    {
        // For now: use inspector slider
        targetSpeed = testSpeed01 * maxSpeedValue;

        currentSpeed = Mathf.MoveTowards(
            currentSpeed,
            targetSpeed,
            (targetSpeed > currentSpeed ? acceleration : deceleration) * Time.deltaTime
        );

        animator.SetFloat("Speed", currentSpeed);
        animator.speed = 1f + (currentSpeed * 0.5f);
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
