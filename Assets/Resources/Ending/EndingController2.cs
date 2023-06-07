using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController2 : MonoBehaviour
{
    [SerializeField] Animator cameraAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartCameraAnimation()
    {
        cameraAnimator.Play("camera_animation");
    }

    public void WhiteScreenAnimation()
    {
        FindObjectOfType<EndingController>().WhiteScreenAnimation();
    }
}
