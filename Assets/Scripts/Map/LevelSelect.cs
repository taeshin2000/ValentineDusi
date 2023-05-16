using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{   [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameMap map;
    [SerializeField] int level;
    private bool isActive;
    void Start(){
        var spriteName = "enemy"+(level+1).ToString()+"-icon";
        int currentProgress = map.currentProgress();
        Debug.Log(currentProgress);
        if (level > currentProgress){
            spriteName += "-black";
            isActive = false;
        }else{
            isActive = true;
        }
        Debug.Log(spriteName);
        spriteRenderer.sprite = Resources.Load<Sprite>("MapIcon/" + spriteName);
;
    }
    private void OnMouseDown()
    {
        if (isActive){
            map.selectLevel(level);
        }
    }

}
