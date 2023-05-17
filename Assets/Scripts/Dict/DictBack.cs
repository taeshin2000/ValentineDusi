using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DictBack : MonoBehaviour
{
    [SerializeField] string transitionID;
    [SerializeField] float loadDelay;
    [SerializeField] EasyTransition.TransitionManager transitionManager;
    void Update()
    {

    }
    public void QuitToMenu()
    {
        AudioManager.instance.Play("ButtonPress");
        Time.timeScale = 1f;
        transitionManager.LoadScene("MapScene",transitionID,loadDelay);
    }

}
