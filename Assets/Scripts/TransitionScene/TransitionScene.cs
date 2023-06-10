using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;


public class TransitionScene : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] string transitionID;
    [SerializeField] float loadDelay;
    [SerializeField] EasyTransition.TransitionManager transitionManager;
    [SerializeField] bool toStart = false;
    void Start()
    {
        if (toStart)
        {
            StartCoroutine("TransitionToStart");
        }
        else
        {
            StartCoroutine("transitionDelay");
        }
    }

    IEnumerator transitionDelay()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(3.5f);
        transitionManager.LoadScene("GameSceneLevel" + level.ToString(), transitionID, loadDelay);
    }

    IEnumerator TransitionToStart()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(6f);
        transitionManager.LoadScene("StartScene2", transitionID, loadDelay);
    }
}
