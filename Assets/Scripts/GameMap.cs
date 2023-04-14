using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMap : MonoBehaviour
{
    [SerializeField] Animator menuAnimator;
    [SerializeField] Transform[] levels;
    [SerializeField] Transform cam;
    [SerializeField] int currentLevel = 1;
    [SerializeField] float speed = 1.0f;
    private bool select = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate ()
	{
        Vector3 newPos;
        if (select){
            newPos = new Vector3(levels[currentLevel].position.x + 4.8f,levels [currentLevel].position.y,-10);
        }else{
            newPos = new Vector3(0,0,-10);
        }
        cam.position = Vector3.Lerp (cam.position, newPos, speed);
	}
    public void selectLevel (int level)
    {
        if (!select){
            select = true;
            menuAnimator.SetBool("select",select);
            currentLevel = level;
        }
    }
    public void onBackClicked(){
        if (select){
            select = false;
            menuAnimator.SetBool("select",select);
        } else {
            SceneManager.LoadScene("Menu");
        }
    }
}
