using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;


public class TransitionScene : MonoBehaviour
{
    [SerializeField] int level ;
    [SerializeField] string transitionID;
    [SerializeField] float loadDelay;
    [SerializeField] EasyTransition.TransitionManager transitionManager;
    void Start()
    {
       StartCoroutine("transitionDelay"); 
    }

    IEnumerator transitionDelay(){
        yield return new WaitForSeconds(5);
        Time.timeScale = 1f;
        transitionManager.LoadScene("GameSceneLevel"+level.ToString(),transitionID,loadDelay);
    }
}
