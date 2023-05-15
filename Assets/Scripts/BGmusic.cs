using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
 
public class BGmusic : MonoBehaviour
{
    public static BGmusic instance;
 
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "MapScene" && currentScene.name != "Dict")
        {
            Destroy(gameObject);
        }
    }
}