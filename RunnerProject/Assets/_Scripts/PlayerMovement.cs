using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerButtonInput input;

    public float maxSpeed = 6f;
    public float finishDistance = 100f;

    float distanceTraveled;
    Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float speed = input.GetSpeed01() * maxSpeed;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (distanceTraveled >= finishDistance)
        {
            Debug.Log(gameObject.name + " FINISHED!");
            enabled = false;
        }
    }

    public float GetProgress01()
    {
        return Mathf.Clamp01(distanceTraveled / finishDistance);
    }
}