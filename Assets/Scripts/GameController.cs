using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

    public bool playerTurn = true;
    int playerPoint = 0;
    public int playerHealth = 3;
    int playerSkillGuage = 0;
    int playerSkillPoint = 0;
    int botPoint = 0;
    public int botHealth = 3;

    public string selectedImg = "";
    private List<string> checkedResult;

    private string target = "";
    private Color32 blue = new Color32 (46,49,126,255);
    private Color32 red = new Color32 (154,31,31,255);
    [SerializeField] Images images;

    [SerializeField] List<Picture> imageList;
    [SerializeField] List<Image> PlayerHearts;
    [SerializeField] List<Image> BotHearts;
    [SerializeField] TextMeshProUGUI last;

    [SerializeField] TextMeshProUGUI wordText;
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] GameObject gameOverMenuUI;
    [SerializeField] TextMeshProUGUI PlayerScore;
    [SerializeField] TextMeshProUGUI BotScore;
    [SerializeField] Button Ability1;
    void Start()
    {
        Ability1.interactable = false;
        if (playerTurn)
        {
            turnText.text = "Player 1's Turn";
            turnText.color = blue;
            turnText.outlineWidth = 0.09f;
            turnText.outlineColor = new Color32(0, 0, 0, 255);
        }
        else
        {
            turnText.text = "Bot's Turn";
            turnText.color = red;
            turnText.outlineWidth = 0.09f;
            turnText.outlineColor = new Color32(0, 0, 0, 255);
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
            if (playerTurn)
            {
                playerHealth -= 1;
            }
            else
            {
                botHealth -= 1;
            }
        }
        else
        {
            if (checkedResult[0] == "master")
            {
                if (playerTurn)
                {
                    playerSkillGuage += 50;
                    checkSkillPoint();
                    playerPoint += (300 * (int)(FindObjectOfType<Timer>().time));
                }
                else
                {
                    botPoint += (300 * (int)(FindObjectOfType<Timer>().time));
                }
                target = checkedResult[1];
                generateBoard(target);
            }
            if (checkedResult[0] == "advanced")
            {
                if (playerTurn)
                {
                    playerSkillGuage += 25;
                    checkSkillPoint();
                    playerPoint += (200 * (int)(FindObjectOfType<Timer>().time));
                }
                else
                {
                    botPoint += (200 * (int)(FindObjectOfType<Timer>().time));
                }
                target = checkedResult[1];
                generateBoard(target);
            }
            if (checkedResult[0] == "basic")
            {
                if (playerTurn)
                {
                    playerSkillGuage += 10;
                    checkSkillPoint();
                    playerPoint += (100 * (int)(FindObjectOfType<Timer>().time));
                }
                else
                {
                    botPoint += (100 * (int)(FindObjectOfType<Timer>().time));
                }
                target = checkedResult[1];
                generateBoard(target);
            }
            toggleTurn();
            FindObjectOfType<Timer>().ResetTimer();
            last.text = target;
            wordText.text = checkedResult[2];
        }
        PlayerScore.text = playerPoint.ToString();
        BotScore.text = botPoint.ToString();
    }

    void checkSkillPoint(){
        if (playerSkillGuage >= 100) {
            playerSkillGuage -= 100;
            playerSkillPoint += 1;
        }
        Debug.Log(playerSkillGuage);
        Debug.Log(playerSkillPoint);
    }

    public void ability1(){
        Ability1.interactable = false;
        playerPoint -= 1;
        Debug.Log("Use Ability!");
    }

    void toggleTurn()
    {
        playerTurn = !playerTurn;
        if (playerTurn)
        {
            if (playerSkillPoint > 0){
                Ability1.interactable = true;
            }
            turnText.text = "Player 1's Turn";
            turnText.color = blue;
        }
        else
        {
            Ability1.interactable = false;
            turnText.text = "Player 2's Turn";
            turnText.color = red;
        }
    }

    void lifePoint()
    {
        if (playerHealth < 1 && PlayerHearts[0] != null)
        {
            Destroy(PlayerHearts[0].gameObject);
        } else if (playerHealth < 2 && PlayerHearts[1] != null)
        {
            Destroy(PlayerHearts[1].gameObject);
        } else if (playerHealth < 3 && PlayerHearts[2] != null)
        {
            Destroy(PlayerHearts[2].gameObject);
        }

        if (botHealth < 1 && BotHearts[0] != null)
        {
            Destroy(BotHearts[0].gameObject);
        } else if (botHealth < 2 && BotHearts[1] != null)
        {
            Destroy(BotHearts[1].gameObject);
        } else if (botHealth < 3 && BotHearts[2] != null)
        {
            Destroy(BotHearts[2].gameObject);
        }
    }
    void gameOver()
    {
        if (playerHealth == 0)
        {
            gameOverMenuUI.SetActive(true);
            gameOverText.text = "Player 2 wins!";
            Time.timeScale = 0f;
        }
        else if (botHealth == 0)
        {
            gameOverMenuUI.SetActive(true);
            gameOverText.text = "Player 1 wins!";
            Time.timeScale = 0f;
        }
    }
}
