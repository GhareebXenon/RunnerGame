using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    public GameObject humanoidRoot;
    public GameObject carRoot;

    public Animator humanoidAnimator;
    //public Animator carAnimator;

    public void SetMode(bool carMode)
    {
        humanoidRoot.SetActive(!carMode);
        carRoot.SetActive(carMode);
    }

    public void SetSpeed(float speed01)
    {
        if (humanoidAnimator)
            humanoidAnimator.SetFloat("Speed", speed01);

       // if (carAnimator)
          //  carAnimator.SetFloat("Speed", speed01);
    }
}
