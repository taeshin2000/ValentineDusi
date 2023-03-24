using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WordImage : MonoBehaviour
{
    public string imgUrl;


    public void onInamgeSelected()
    {
        FindObjectOfType<GameController>().result = imgUrl;
    }
}
