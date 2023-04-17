using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneAnimationController : MonoBehaviour
{
    [SerializeField] GameController gameController;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void displayTargetPic()
    {
        gameController.displayTargetPic();
    }

    public void disableTatgetPic()
    {
        gameController.disableTargetPic();
    }
}
