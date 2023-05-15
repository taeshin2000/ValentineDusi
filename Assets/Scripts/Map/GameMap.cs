using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameMap : MonoBehaviour
{
    [SerializeField] Animator menuAnimator;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI levelDescText;
    [SerializeField] Transform[] levels;
    [SerializeField] Transform cam;
    [SerializeField] int currentLevel = 1;
    [SerializeField] float speed = 1.0f;
    private bool select = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
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
        levelText.text = "Level " + (currentLevel + 1).ToString();
        levelDesc();
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
            SceneManager.LoadScene("StartScene2");
        }
    }

    public void playLevel()
    {
        if (currentLevel == 0)
        {

        }
        if (currentLevel == 1)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameSceneLevel2");
        }
        if (currentLevel == 2)
        {

        }
    }

    public void levelDesc()
    {
        if (currentLevel == 0)
        {
            levelDescText.text = "The twins of doom approaches!";
        }
        if (currentLevel == 1)
        {
            levelDescText.text = "What is that shy mascot girl from across the street doing...?";
        }
        if (currentLevel == 2)
        {
            levelDescText.text = "You are finally facing the senpai that you've been searching for.";
        }
    }
}
