using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningController : MonoBehaviour
{
    [SerializeField] Animator scene2Animator;
    [SerializeField] Animator mcScene2Animator;
    [SerializeField] Animator canvasAnimator;
    [SerializeField] Animator cameraAnimator;
    [SerializeField] Animator scene3Animator;
    [SerializeField] Animator calenderAnimator;
    [SerializeField] Animator scene4Animator;

    void Start()
    {

    }


    void Update()
    {

    }

    public void Scene1CameraTransitionComplete()
    {
        scene2Animator.Play("scene2_animation");
    }

    public void startTextScene2()
    {
        canvasAnimator.Play("scene2_text");
    }

    public void StratTextScene3()
    {
        canvasAnimator.Play("scene3_text");
    }

    public void StartCameraTransitionToScene3()
    {
        cameraAnimator.Play("camera_to_scene3");
    }

    public void StartScene3Animation()
    {
        scene3Animator.Play("scene3_animation");
    }

    public void TransitionScene3()
    {
        scene3Animator.Play("scene3_end");
        calenderAnimator.Play("calender_end");
    }

    public void StartScene4Animation()
    {
        scene4Animator.Play("scene4_start");
    }
}
