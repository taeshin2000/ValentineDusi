using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndingButton : MonoBehaviour
{
    [SerializeField] GameMap map;
    [SerializeField] Button button;
    void Start()
    {
        int currentProgress = map.currentProgress();
        Debug.Log(currentProgress);
        if (currentProgress == 3 ){
            button.gameObject.SetActive(true);
        } else{
            button.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
