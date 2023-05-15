using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseboard;
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    public Image pauseButton;


    void start(){
        pauseboard.SetActive(false);
        pauseButton.sprite = Resources.Load<Sprite>("UI/Button/pause");
    }
    void Update()
    {
    }

    public void Resume()
    {
        AudioManager.instance.Play("ButtonPress");
        pauseboard.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        FindObjectOfType<GameController>().EnableGameBoard();
        if (FindObjectOfType<GameController>().boardActive){
            pauseButton.sprite = Resources.Load<Sprite>("UI/Button/pause");
        }
    }
    public void Pause()
    {
        AudioManager.instance.Play("ButtonPress");
        pauseboard.SetActive(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        FindObjectOfType<GameController>().DisableGameBoard();
        if (FindObjectOfType<GameController>().boardActive){
            pauseButton.sprite = Resources.Load<Sprite>("UI/Button/play");
        }

    }

    public void Toggle(){
         if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
    }

    public void Retry()
    {
        AudioManager.instance.Play("ButtonPress");
        gameOverMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void QuitToMenu()
    {
        AudioManager.instance.Play("ButtonPress");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MapScene");
    }
}
