using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningButton : MonoBehaviour
{
    [SerializeField] string transitionID;
    [SerializeField] float loadDelay;
    [SerializeField] EasyTransition.TransitionManager transitionManager;
    public void ToMapScene()
    {
        transitionManager.LoadScene("MapScene",transitionID,loadDelay);
    }
}
