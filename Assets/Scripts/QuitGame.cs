using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public GameObject quitMenuUI;
    public void QuitGameMenu()
    {
        AudioManager.instance.Play("ButtonPress");
        quitMenuUI.SetActive(true);
    }
    public void QuitYes()
    {
        AudioManager.instance.Play("ButtonPress");
        Application.Quit();
    }
    public void QuitNo()
    {
        AudioManager.instance.Play("ButtonPress");
        quitMenuUI.SetActive(false);
    }
}
