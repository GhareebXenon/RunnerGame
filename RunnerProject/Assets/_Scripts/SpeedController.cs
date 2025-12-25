using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public Animator animator;
    public PlayerButtonInput input;

    public float maxAnimatorSpeed = 2f;

    void Update()
    {
        float animSpeed = input.GetSpeed01() * maxAnimatorSpeed;

        animator.SetFloat("Speed", animSpeed);
        animator.speed = 1f + animSpeed * 0.5f;
    }
}
