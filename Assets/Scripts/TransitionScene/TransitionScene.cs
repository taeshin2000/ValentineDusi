using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    [SerializeField] int level ;
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine("transitionDelay"); 
    }

    IEnumerator transitionDelay(){
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameSceneLevel"+level.ToString());
    }
}
