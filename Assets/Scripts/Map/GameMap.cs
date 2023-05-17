using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using EasyTransition;


public class GameMap : MonoBehaviour,IDataPersistence
{
    [SerializeField] Animator menuAnimator;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI levelDescText;
    [SerializeField] TextMeshProUGUI levelHighScore;
    [SerializeField] TextMeshProUGUI levelClear;
    [SerializeField] Transform[] levels;
    [SerializeField] Transform cam;
    [SerializeField] int currentLevel = 1;
    [SerializeField] float speed = 1.0f;
    [SerializeField] AudioSource mapBGM;
    private bool select = false;

    [SerializeField] string transitionID;
    [SerializeField] float loadDelay;
    [SerializeField] EasyTransition.TransitionManager transitionManager;

    //load data
    [SerializeField] List<Level> levelsData = new List<Level>();
    public void LoadData(GameData gameData)
    {
        this.levelsData = gameData.levels;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.levels = this.levelsData;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        DataPersistenceManager.instance.LoadGame();
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
        if (levelsData[currentLevel].highScore == 0) {
            levelHighScore.text = "High Score : -";
        }else{
            levelHighScore.text = "High Score : " + levelsData[currentLevel].highScore.ToString();
        }
        if (levelsData[currentLevel].clear){
            levelClear.text = "level cleared !";
        }else{
            levelClear.text = "not clear yet :/";
        }
        levelDesc();
	}
    public void selectLevel (int level)
    {
        AudioManager.instance.Play("ButtonPress");
        if (!select){
            select = true;
            menuAnimator.SetBool("select",select);
            currentLevel = level;
        }
    }
    public void onBackClicked(){
        AudioManager.instance.Play("ButtonPress");
        if (select){
            select = false;
            menuAnimator.SetBool("select",select);
        } else {
            transitionManager.LoadScene("StartScene2",transitionID,loadDelay);
        }
    }

    public void onDictClicked(){
        AudioManager.instance.Play("DictPage");
        transitionManager.LoadScene("Dict",transitionID,loadDelay);
    }

    public void playLevel()
    {
        AudioManager.instance.Play("ButtonPress");
        Time.timeScale = 1f;
        transitionManager.LoadScene("Level"+(currentLevel+1).ToString()+"TransitionScene",transitionID,loadDelay);
    }

    public void levelDesc()
    {
        if (currentLevel == 0)
        {
            levelDescText.text = "The twins of doom approaches!";
        }
        if (currentLevel == 1)
        {
            levelDescText.text = "";
        }
        if (currentLevel == 2)
        {
            levelDescText.text = "You are finally facing the senpai that you've been searching for.";
        }
    }
    public int currentProgress(){
        if (levelsData[2].clear == true){
            return 3;
        }
        if (levelsData[1].clear == true){
            return 2;
        }
        if (levelsData[0].clear == true){
            return 1;
        }
        else{
            return 0;
        }
    }
}
