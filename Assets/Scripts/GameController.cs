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
    private Color32 blue = new Color32 (46,49,126,255);
    private Color32 red = new Color32 (154,31,31,255);
    [SerializeField] Images images;

    [SerializeField] List<Picture> imageList;
    [SerializeField] List<Image> P1Hearts;
    [SerializeField] List<Image> P2Hearts;
    [SerializeField] TextMeshProUGUI last;

    [SerializeField] TextMeshProUGUI wordText;
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] GameObject gameOverMenuUI;
    [SerializeField] TextMeshProUGUI P1Score;
    [SerializeField] TextMeshProUGUI P2Score;
    void Start()
    {
        if (player1Turn)
        {
            Debug.Log("Player 1 Turn");
            turnText.text = "Player 1's Turn";
            turnText.color = blue;
            turnText.outlineWidth = 0.09f;
            turnText.outlineColor = new Color32(0, 0, 0, 255);
        }
        else
        {
            Debug.Log("Player 2 Turn");
            turnText.text = "Player 2's Turn";
            turnText.color = red;
            turnText.outlineWidth = 0.09f;
            turnText.outlineColor = new Color32(0, 0, 0, 255);
        }
        Word word = images.randomWord();
        target = word.last;
        wordText.text = word.word;
        last.text = word.last;
        generateBoard(target);
        Debug.Log("Test");
    }

    void Update()
    {
        gameOver();
        lifePoint();
    }

    public void setupImage(List<Images.wordBoard> url)
    {
        for (int i = 0; i < 9; i++)
        {
            //Debug.Log(i);
            //Debug.Log(url[i]);
            //Debug.Log(imageList[i].GetType());
            imageList[i].imgUrl = url[i].name;
            // TEMP CHECK TIER
            if (url[i].tier != "random"){
                Debug.Log(url[i].tier+":"+ url[i].name);
            }
            imageList[i].button.image.sprite = Resources.Load<Sprite>("images/" + url[i].name);
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
        P1Score.text = player1Point.ToString();
        P2Score.text = player2Point.ToString();
    }
    void toggleTurn()
    {
        player1Turn = !player1Turn;
        if (player1Turn)
        {
            turnText.text = "Player 1's Turn";
            Debug.Log("Player 1 Turn");
            turnText.color = blue;
        }
        else
        {
            turnText.text = "Player 2's Turn";
            Debug.Log("Player 2 Turn");
            turnText.color = red;
        }
    }

    void lifePoint()
    {
        if (player1Health < 1 && P1Hearts[0] != null)
        {
            Destroy(P1Hearts[0].gameObject);
        } else if (player1Health < 2 && P1Hearts[1] != null)
        {
            Destroy(P1Hearts[1].gameObject);
        } else if (player1Health < 3 && P1Hearts[2] != null)
        {
            Destroy(P1Hearts[2].gameObject);
        }

        if (player2Health < 1 && P2Hearts[0] != null)
        {
            Destroy(P2Hearts[0].gameObject);
        } else if (player2Health < 2 && P2Hearts[1] != null)
        {
            Destroy(P2Hearts[1].gameObject);
        } else if (player2Health < 3 && P2Hearts[2] != null)
        {
            Destroy(P2Hearts[2].gameObject);
        }
    }
    void gameOver()
    {
        if (player1Health == 0)
        {
            Debug.Log("Player 2 win!!!");
            gameOverMenuUI.SetActive(true);
            gameOverText.text = "Player 2 wins!";
            Time.timeScale = 0f;
        }
        else if (player2Health == 0)
        {
            Debug.Log("Player 1 win!!!");
            gameOverMenuUI.SetActive(true);
            gameOverText.text = "Player 1 wins!";
            Time.timeScale = 0f;
        }
    }
}
