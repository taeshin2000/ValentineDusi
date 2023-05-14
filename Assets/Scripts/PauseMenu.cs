using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    public Image pauseButton;

    void start(){
        pauseButton.sprite = Resources.Load<Sprite>("UI/Button/pause");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        FindObjectOfType<GameController>().EnableGameBoard();
        pauseButton.sprite = Resources.Load<Sprite>("UI/Button/pause");
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        FindObjectOfType<GameController>().DisableGameBoard();
        pauseButton.sprite = Resources.Load<Sprite>("UI/Button/play");

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
        gameOverMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
}
