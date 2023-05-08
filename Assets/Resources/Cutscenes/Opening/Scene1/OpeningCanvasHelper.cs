using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCanvasHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartCameraTransitionToScene3()
    {
        FindObjectOfType<OpeningController>().StartCameraTransitionToScene3();
    }

    public void StartScene3Animation()
    {
        FindObjectOfType<OpeningController>().StartScene3Animation();
    }

    public void TransitionScene3()
    {
        FindObjectOfType<OpeningController>().TransitionScene3();
    }

    public void StartScene4Animation()
    {
        FindObjectOfType<OpeningController>().StartScene4Animation();
    }
}
