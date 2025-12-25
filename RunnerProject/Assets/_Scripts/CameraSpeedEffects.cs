using UnityEngine;

public class CameraSpeedEffects : MonoBehaviour
{
    public PlayerButtonInputEsp input;

    public float normalFOV = 60f;
    public float maxFOV = 75f;
    public float speedThreshold = 0.6f;
    public float smooth = 5f;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        float targetFOV = input.speed02 > speedThreshold ? maxFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * smooth);
    }
}
