using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] bool countUp;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] float inputTimeInSec;
    public float time;
    public float timeDuration;





    void Start()
    {
        timeDuration = inputTimeInSec;
        ResetTimer();
    }

    void Update()
    {
        if (countUp && (time < timeDuration))
        {
            time += Time.deltaTime;
            UpdateTimerDisplay(time);
        }
        else if (!countUp && (time > 0))
        {
            time -= Time.deltaTime;
            UpdateTimerDisplay(time);
        }
        else
        {
            TimeUp();
        }
    }

    void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}:{1:00}", minutes, seconds);
        timeText.text = currentTime;
    }

    public void ResetTimer()
    {
        if (countUp)
        {
            time = 0;
        }
        else
        {
            time = timeDuration;
        }
    }

    void TimeUp()
    {
        timeText.text = "Time UP !!!";
        bool player1turn = FindObjectOfType<GameController>().player1Turn;
        if (player1turn)
        {
            FindObjectOfType<GameController>().player1Health = 0;
        }
        else
        {
            FindObjectOfType<GameController>().player2Health = 0;
        }


    }

    public float getTime()
    {
        return time;
    }
}