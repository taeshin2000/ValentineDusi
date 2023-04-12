using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{   
    [SerializeField] GameMap map;
    [SerializeField] int level = 1;
    private void OnMouseDown()
    {
        Debug.Log(level);
        map.selectLevel(level);
    }

}
