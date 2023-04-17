using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] Animator boardAnimator;
    [SerializeField] List<GameObject> imageList;
    [SerializeField] GameController gameController;
    private Timer gameTimer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void genboardAnimation(Timer timer)
    {
        gameTimer = timer;
        foreach (var item in imageList)
        {
            item.SetActive(false);
        }
        IEnumerator genboardDelay()
        {
            yield return new WaitForSeconds(3);
            foreach (var item in imageList)
            {
                item.SetActive(true);
            }
            boardAnimator.Play("genBoard");
        }
        StartCoroutine(genboardDelay());
    }

    public void ResetTimer()
    {
        gameTimer.ResetTimer();
        gameController.picPressed = false;
    }

    public void setActive(bool set)
    {
        foreach (var item in imageList)
        {
            item.SetActive(set);
        }
    }
}
