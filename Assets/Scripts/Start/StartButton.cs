using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartButton : MonoBehaviour
{
    [SerializeField] string transitionID;
    [SerializeField] float loadDelay;
    [SerializeField] EasyTransition.TransitionManager transitionManager;
    [SerializeField] bool playCutScene;
    public void StartGame()
    {
        if (playCutScene)
        {
            transitionManager.LoadScene("OpeningCutscene", transitionID, loadDelay);
        }
        else
        {
            transitionManager.LoadScene("MapScene", transitionID, loadDelay);
        }
        AudioManager.instance.Play("ButtonPress");
    }
}
