using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] Bot noobBotV1;
    //for Bot
    private float timeToAnswer = 0;
    private string botAnswerTier = "";
    [SerializeField] MainCharacter mainChracter;
    [SerializeField] Enemy2 enemy2;

    [SerializeField] Timer timer;
    public int maxTurn = 20;
    public int curTurn = 1;
    public bool picPressed = false;

    public bool playerTurn = true;
    int playerPoint = 0;
    public int playerHealth = 3;
    public int playerSkillGuage = 0;
    public int maxPlayerSkillGauge = 100;
    public int playerSkillPoint = 0;
    int botPoint = 0;
    public int botHealth = 3;

    public string selectedImg = "";
    private List<string> checkedResult;
    private List<string> checkedAllResults;

    private string target = "";
    private Color32 blue = new Color32(46, 49, 126, 255);
    private Color32 red = new Color32(154, 31, 31, 255);
    [SerializeField] Images images;
    [SerializeField] List<Picture> imageList;
    [SerializeField] Image pressedWordImage;
    [SerializeField] Image previousWordImage;
    [SerializeField] WordImage startingWord;
    [SerializeField] List<Image> PlayerHearts;
    [SerializeField] List<Image> BotHearts;
    [SerializeField] List<string> cururls;
    [SerializeField] TextMeshProUGUI last;
    [SerializeField] TextMeshProUGUI wordText;
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] TextMeshProUGUI resultPlayer;
    [SerializeField] TextMeshProUGUI resultBot;
    [SerializeField] GameObject gameOverMenuUI;
    [SerializeField] GameObject resultMenuUI;
    [SerializeField] GameObject pressedInfoUI;
    [SerializeField] TextMeshProUGUI PlayerScore;
    [SerializeField] TextMeshProUGUI BotScore;    
    [SerializeField] TextMeshProUGUI wordPressed;
    [SerializeField] Button Ability1;
    void Start()
    {
        foreach (var item in imageList)
        {
            item.button.interactable = true;
        }
        Ability1.interactable = false;
        startingWord = images.randomWord();
        Word word = startingWord.words[0];
        target = word.last;
        picPressed = true;
        wordPressed.text = word.word;
        startingPicShow();
        wordText.text = word.word;
        last.text = word.last;
        pressedWordImage.sprite = Resources.Load<Sprite>("images/" + startingWord.name);
    }

    void Update()
    {
        lifePoint();
        turnText.text = curTurn.ToString() + "/" + maxTurn.ToString(); 
    }
    public void startingPicShow()
    {
        IEnumerator WaitForPicShow()
        {
            pressedInfoUI.SetActive(true);
            yield return new WaitForSeconds(3);
            pressedInfoUI.SetActive(false);
            picPressed = false;
            previousWordImage.sprite = Resources.Load<Sprite>("images/" + startingWord.name);
            generateBoard(target);
        }
        StartCoroutine(WaitForPicShow());
    }
    public void setupImage(List<Images.wordBoard> url)
    {
        for (int i = 0; i < 9; i++)
        {
            //Debug.Log(i);
            //Debug.Log(url[i]);
            //Debug.Log(imageList[i].GetType());
            imageList[i].imgUrl = url[i].name;
            imageList[i].tier = url[i].tier;
            // TEMP CHECK TIER
            if (url[i].tier != "random")
            {
                Debug.Log(url[i].tier + ":" + url[i].name);
            }
            imageList[i].button.image.sprite = Resources.Load<Sprite>("images/" + url[i].name);
        }
    }

    void generateBoard(string target)
    {
        var urls = images.generateBoard(target);
        cururls.Clear();
        foreach (var tempurl in urls)
        {
            cururls.Add(tempurl.name);
        }
        setupImage(urls);
    }

    public void ImageSelected()
    {
        //Debug.Log("gameController");
        checkedResult = images.checkResult(selectedImg, target);
        picPressed = true;
        if (checkedResult[0] == "failed")
        {
            if (playerTurn)
            {
                mainChracter.ToggleWrong();
                playerHealth -= 1;
            }
            else
            {
                botHealth -= 1;
            }
        }
        else
        {
            pressedWordImage.sprite = Resources.Load<Sprite>("images/" + selectedImg);
            if (checkedResult[0] == "master")
            {
                if (playerTurn)
                {
                    mainChracter.ToggleCorrect();
                    playerSkillGuage += 50;
                    checkSkillPoint();
                    playerPoint += (300 * (int)(timer.time));
                }
                else
                {
                    botPoint += (300 * (int)(timer.time));
                }
            }
            if (checkedResult[0] == "advanced")
            {
                if (playerTurn)
                {
                    mainChracter.ToggleCorrect();
                    playerSkillGuage += 25;
                    checkSkillPoint();
                    playerPoint += (200 * (int)(timer.time));
                }
                else
                {
                    botPoint += (200 * (int)(timer.time));
                }
            }
            if (checkedResult[0] == "basic")
            {
                if (playerTurn)
                {
                    mainChracter.ToggleCorrect();
                    playerSkillGuage += 10;
                    checkSkillPoint();
                    playerPoint += (100 * (int)(timer.time));
                }
                else
                {
                    botPoint += (100 * (int)(timer.time));
                }
            }
            target = checkedResult[1];
            wordPressed.text = checkedResult[2];
            IEnumerator WaitForPicShow()
            {
                pressedInfoUI.SetActive(true);
                yield return new WaitForSeconds(3);
                pressedInfoUI.SetActive(false);
                picPressed = false;
                toggleTurn();
                timer.ResetTimer();
                last.text = target;
                wordText.text = checkedResult[2];
                previousWordImage.sprite = Resources.Load<Sprite>("images/" + selectedImg);
                generateBoard(target);
            }
            StartCoroutine(WaitForPicShow());    
        }
        PlayerScore.text = playerPoint.ToString();
        BotScore.text = botPoint.ToString();
    }

    void checkSkillPoint()
    {
        if (playerSkillGuage >= maxPlayerSkillGauge)
        {
            playerSkillGuage -= maxPlayerSkillGauge;
            playerSkillPoint += 1;
        }
        Debug.Log(playerSkillGuage);
        Debug.Log(playerSkillPoint);
    }

    public void ability1()
    {
        Ability1.interactable = false;
        playerSkillPoint -= 1;
        checkedAllResults = images.checkAllResults(cururls,target);
        int index = checkedAllResults.IndexOf("master");
        if (index == -1)
        {
            index = checkedAllResults.IndexOf("advanced");
        }
        else if (index == -1)
        {
            index = checkedAllResults.IndexOf("basic");
        }
        Debug.Log(cururls[index]);
        imageList[index].button.image.color = Color.green;
        Debug.Log("Use Ability!");
    }

    void toggleTurn()
    {
        if (curTurn >= maxTurn)
        {
            results();
        }
        else
        {
            playerTurn = !playerTurn;
            curTurn += 1;
            for (int i = 0; i < 9; i++)
            {
                imageList[i].button.image.color = Color.white;
            }
            if (playerTurn)
            {
                foreach (var item in imageList)
                {
                    item.button.interactable = true;
                }
                if (playerSkillPoint > 0)
                {
                    Ability1.interactable = true;
                }
            }
            else
            {
                foreach (var item in imageList)
                {
                    item.button.interactable = false;
                }
                Ability1.interactable = false;
                timeToAnswer = noobBotV1.CalculateTime();
                if (timeToAnswer > 7.0f){
                    enemy2.ToggleThink();
                }
                botAnswerTier = noobBotV1.CalculateAnswer();
                Debug.Log(timeToAnswer);
                Debug.Log(botAnswerTier);
                StartCoroutine("BotAnswer");

            }
        }
    }

    void lifePoint()
    {
        if (playerHealth < 1 && PlayerHearts[0] != null)
        {
            gameOver();
            Destroy(PlayerHearts[0].gameObject);
        }
        else if (playerHealth < 2 && PlayerHearts[1] != null)
        {
            Destroy(PlayerHearts[1].gameObject);
        }
        else if (playerHealth < 3 && PlayerHearts[2] != null)
        {
            Destroy(PlayerHearts[2].gameObject);
        }

        if (botHealth < 1 && BotHearts[0] != null)
        {
            gameOver();
            Destroy(BotHearts[0].gameObject);
        }
        else if (botHealth < 2 && BotHearts[1] != null)
        {
            Destroy(BotHearts[1].gameObject);
        }
        else if (botHealth < 3 && BotHearts[2] != null)
        {
            Destroy(BotHearts[2].gameObject);
        }
    }
    void results()
    {
        resultPlayer.text = playerPoint.ToString();
        resultBot.text = botPoint.ToString();
        Time.timeScale = 0f;
        resultMenuUI.SetActive(true);
        if (playerPoint > botPoint)
        {
            resultPlayer.color = Color.yellow;
            resultText.text = "You win!";
        }
        else if (botPoint > playerPoint)
        {
            resultBot.color = Color.yellow;
            resultText.text = "You lose!";
        }
        else if (playerPoint == botPoint)
        {
            resultText.text = "It's a Draw!";
        }
    }
    void gameOver()
    {
        if (playerHealth == 0)
        {
            gameOverMenuUI.SetActive(true);
            gameOverText.text = "You lose!";
            Time.timeScale = 0f;
        }
        else if (botHealth == 0)
        {
            gameOverMenuUI.SetActive(true);
            gameOverText.text = "You win!";
            Time.timeScale = 0f;
        }
    }

    IEnumerator BotAnswer()
    {
        yield return new WaitForSeconds(timeToAnswer);
        Debug.Log("Hi");
        var finish = false;
        foreach (var item in imageList)
        {
            if (botAnswerTier == "wrong")
            {
                if (item.tier == "random")
                {
                    selectedImg = item.imgUrl;
                    Debug.Log(selectedImg);
                    ImageSelected();
                    break;
                }
            }
            else
            {
                if (botAnswerTier == item.tier)
                {
                    selectedImg = item.imgUrl;
                    Debug.Log(selectedImg);
                    ImageSelected();
                    finish = true;
                    break;
                }
            }
        }
        if (!finish)
        {
            var hasBasic = false;
            var hasAdvance = false;
            var hasMaster = false;
            foreach (var item in imageList)
            {
                if (item.tier == "basic")
                {
                    hasBasic = true;
                    selectedImg = item.imgUrl;
                    Debug.Log(selectedImg);
                    ImageSelected();
                    finish = true;
                    break;
                }
            }
            if (!hasBasic){
                foreach (var item in imageList) {
                    if (item.tier == "advanced")
                    {
                        hasAdvance = true;
                        selectedImg = item.imgUrl;
                        Debug.Log(selectedImg);
                        ImageSelected();
                        finish = true;
                        break;
                    }
                }
            }
            if (!hasAdvance){
                foreach (var item in imageList) {
                    if (item.tier == "master")
                    {
                        hasMaster = true;
                        selectedImg = item.imgUrl;
                        Debug.Log(selectedImg);
                        ImageSelected();
                        finish = true;
                        break;
                    }
                }
            }
            if (!hasMaster){
                Debug.Log("!!!!NO ANSWER!!!!");
            }
        }
        enemy2.ToggleAnswer();

    }

}
