using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public Animator animator;
    public PlayerButtonInput input;

    public float maxAnimatorSpeed = 2f;
    public float dampTime = 0.25f; // THIS is the magic
    public float animSpeedMultiplier = 0.5f;

    void Update()
    {
        float targetSpeed = input.GetSpeed01() * maxAnimatorSpeed;

        // Smoothly blends between walk  run
        animator.SetFloat("Speed", targetSpeed, dampTime, Time.deltaTime);

        // Smooth animation playback speed
        animator.speed = Mathf.Lerp(
            animator.speed,
            1f + targetSpeed * animSpeedMultiplier,
            Time.deltaTime * 5f
        );
    }
}
