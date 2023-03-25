using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

    public bool player1Turn = true;
    int player1Point = 0;
    public int player1Health = 3;
    int player2Point = 0;
    public int player2Health = 3;

    public string selectedImg = "";
    private List<string> checkedResult;

    private string target = "";

    [SerializeField] Images images;

    [SerializeField] List<Picture> imageList;
    [SerializeField] TextMeshProUGUI last;

    [SerializeField] TextMeshProUGUI wordText;

    void Start()
    {
        if (player1Turn)
        {
            Debug.Log("Player 1 Turn");
        }
        else
        {
            Debug.Log("Player 2 Turn");
        }
        Word word = images.randomWord();
        target = word.last;
        wordText.text = word.word;
        last.text = word.last;
        generateBoard(target);
    }

    void Update()
    {
        gameOver();
    }

    public void setupImage(List<string> url)
    {
        for (int i = 0; i < 9; i++)
        {
            //Debug.Log(i);
            //Debug.Log(url[i]);
            //Debug.Log(imageList[i].GetType());
            imageList[i].imgUrl = url[i];
            imageList[i].button.image.sprite = Resources.Load<Sprite>("images/" + url[i]);
        }
    }

    void generateBoard(string target)
    {
        var urls = images.generateBoard(target);
        setupImage(urls);
    }

    public void ImageSelected()
    {
        //Debug.Log("gameController");
        checkedResult = images.checkResult(selectedImg, target);

        if (checkedResult[0] == "failed")
        {
            if (player1Turn)
            {
                player1Health -= 1;
                Debug.Log("Player 1 : wrong");
            }
            else
            {
                player2Health -= 1;
                Debug.Log("Player 2 : wrong");
            }
        }
        else
        {
            if (checkedResult[0] == "master")
            {
                if (player1Turn)
                {
                    player1Point += (300 * (int)(FindObjectOfType<Timer>().time));
                    Debug.Log("Player 1 point : " + player1Point.ToString());
                }
                else
                {
                    player2Point += (300 * (int)(FindObjectOfType<Timer>().time));
                    Debug.Log("Player 2 point : " + player2Point.ToString());
                }
                target = checkedResult[1];
                generateBoard(target);
            }
            if (checkedResult[0] == "advanced")
            {
                if (player1Turn)
                {
                    player1Point += (200 * (int)(FindObjectOfType<Timer>().time));
                    Debug.Log("Player 1 point : " + player1Point.ToString());
                }
                else
                {
                    player2Point += (200 * (int)(FindObjectOfType<Timer>().time));
                    Debug.Log("Player 2 point : " + player2Point.ToString());
                }
                target = checkedResult[1];
                generateBoard(target);
            }
            if (checkedResult[0] == "basic")
            {
                if (player1Turn)
                {
                    player1Point += (100 * (int)(FindObjectOfType<Timer>().time));
                    Debug.Log("Player 1 point : " + player1Point.ToString());
                }
                else
                {
                    player2Point += (100 * (int)(FindObjectOfType<Timer>().time));
                    Debug.Log("Player 2 point : " + player2Point.ToString());
                }
                target = checkedResult[1];
                generateBoard(target);
            }
            toggleTurn();
            FindObjectOfType<Timer>().ResetTimer();
            last.text = target;
            wordText.text = checkedResult[2];
        }

    }
    void toggleTurn()
    {
        player1Turn = !player1Turn;
        if (player1Turn)
        {
            Debug.Log("Player 1 Turn");
        }
        else
        {
            Debug.Log("Player 2 Turn");
        }
    }

    void gameOver()
    {
        if (player1Health == 0)
        {
            Debug.Log("Playe 2 win!!!");
        }
        else if (player2Health == 0)
        {
            Debug.Log("Player 1 win!!!");
        }
    }
}
