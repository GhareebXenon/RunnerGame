using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerButtonInputEsp input;
    public float maxSpeed = 6f;

    bool canMove = true;

    void Update()
    {
        if (!canMove) return;

        float speed = input.GetSpeed02() * maxSpeed;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void Stop()
    {
        canMove = false;
    }
}