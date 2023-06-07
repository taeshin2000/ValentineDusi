using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    [SerializeField] GameObject dict;
    [SerializeField] Animator finalSceneAnimator;
    [SerializeField] Animator canvasAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableDict()
    {
        dict.SetActive(false);
    }

    public void StartFinalScene()
    {
        finalSceneAnimator.Play("Final_scene_start");
    }

    public void WhiteScreenAnimation()
    {
        canvasAnimator.Play("white_screen_animation");
    }
}
