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

    [SerializeField] Enemy1 enemy1;

    [SerializeField] Timer timer;
    public int maxTurn = 20;
    public int curTurn = 1;
    public bool picPressed = false;

    public bool playerTurn = true;
    int playerPoint = 0;
    public int playerHealth = 3;
    public float playerSkillGuage = 0;
    public float targetPlayerSkillGauge = 0;
    public float maxPlayerSkillGauge = 100;
    public int playerSkillPoint = 0;
    public int multiplier = 1;
    public float timeMultiplier = 1;
    int botPoint = 0;
    public int botHealth = 3;

    public string selectedImg = "";
    public int selectedImgIndex = -1;
    private List<string> checkedResult;
    private List<string> checkedAllResults;

    public bool boardActive = false;

    private string target = "";
    //private Color32 blue = new Color32(46, 49, 126, 255);
    //private Color32 red = new Color32(154, 31, 31, 255);
    [SerializeField] Board gameBoard;
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
    [SerializeField] TextMeshProUGUI skillPoint;
    [SerializeField] TextMeshProUGUI timeBonusScoreText;
    [SerializeField] GameObject gameOverMenuUI;
    [SerializeField] GameObject resultMenuUI;
    [SerializeField] GameObject pressedInfoUI;
    [SerializeField] Animator presseedInfoAnimator;
    [SerializeField] Animator playerUIanimator;
    [SerializeField] Animator enemyUIanimator;
    [SerializeField] Animator skill1Animator;
    [SerializeField] Animator skill2Animator;
    [SerializeField] Animator skill3Animator;
    [SerializeField] TextMeshProUGUI PlayerScore;
    [SerializeField] TextMeshProUGUI BotScore;
    [SerializeField] TextMeshProUGUI wordPressed;
    [SerializeField] TextMeshProUGUI wordPressedPoint;
    [SerializeField] Button Ability1;
    [SerializeField] Button Ability2;
    [SerializeField] Button Ability3;
    [SerializeField] GameObject targetWord;
    //for score number animation or numAnim for short
    public int numAnimFPS = 30;
    public float numAnimDuration = 1f;
    [SerializeField] Coroutine numAnimCoroutine;
    void Start()
    {
        Time.timeScale = 1f;
        foreach (var item in imageList)
        {
            item.button.interactable = true;
        }
        disableAbilityButton();
        startingWord = images.randomWord();
        Word word = startingWord.words[0];
        target = word.last;
        picPressed = true;
        wordPressed.text = word.word;
        gameBoard.setActive(false);
        boardActive = false;
        StartCoroutine(startGame());
        wordText.text = word.word;
        last.text = word.last;
        pressedWordImage.sprite = Resources.Load<Sprite>("images/" + startingWord.name);
        skill1Animator.SetTrigger("idle");
        skill3Animator.Play("skill3idle");
        skill2Animator.Play("skill2idle");
    }

    void Update()
    {
        lifePoint();
        gameOver();
        UImanager();
        checkSkillPoint();
        timeMultiplier = Mathf.Ceil((timer.time / timer.timeDuration) * 5);
        turnText.text = curTurn.ToString() + "/" + maxTurn.ToString();
        skillPoint.text = playerSkillPoint.ToString();
        timeBonusScoreText.text = "x" + ((int)(timeMultiplier)).ToString();
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(3);
        startingPicShow();
        gameBoard.genboardAnimation(timer);
    }
    public void startingPicShow()
    {
        Debug.Log("start");
        IEnumerator WaitForPicShow()
        {
            pressedInfoUI.SetActive(true);
            yield return new WaitForSeconds(3);
            pressedInfoUI.SetActive(false);
            //picPressed = false;
            previousWordImage.sprite = Resources.Load<Sprite>("images/" + startingWord.name);
            generateBoard(target);
            gameBoard.setActive(true);
            boardActive = true;
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
        int previousPlayerScore = playerPoint;
        int previousBotScore = botPoint;
        checkedResult = images.checkResult(selectedImg, target, playerTurn);
        if (checkedResult[0] == "failed")
        {
            if (playerTurn)
            {
                mainChracter.ToggleWrong();
                playerHealth -= 1;
                gameBoard.wrongAimation(selectedImgIndex);
            }
            else
            {
                botHealth -= 1;
            }
            enableAbilityButton();
        }
        else
        {
            skill1Animator.SetTrigger("idle");
            skill2Animator.SetTrigger("idle");
            int pointGet = 0;
            disableAbilityButton();
            picPressed = true;
            pressedWordImage.sprite = Resources.Load<Sprite>("images/" + selectedImg);
            if (checkedResult[0] == "master")
            {
                if (checkedResult[3] != "collected" && playerTurn)
                {
                    presseedInfoAnimator.Play("Master_new");
                }
                else if (playerTurn)
                {
                    presseedInfoAnimator.Play("Master_player");
                }
                else
                {
                    presseedInfoAnimator.Play("Master");
                }
                if (playerTurn)
                {
                    mainChracter.ToggleCorrect();
                    targetPlayerSkillGauge += 50;
                    if (multiplier != 1)
                    {
                        Debug.Log(300 * (int)(timeMultiplier));
                        Debug.Log(300 * (int)(timeMultiplier) * multiplier);
                    }
                    pointGet = (300 * (int)(timeMultiplier) * multiplier);
                    playerPoint += pointGet;
                }
                else
                {
                    pointGet = (300 * (int)(timeMultiplier));
                    botPoint += pointGet;
                }
            }
            if (checkedResult[0] == "advanced")
            {
                if (checkedResult[3] != "collected" && playerTurn)
                {
                    presseedInfoAnimator.Play("Advance_new");
                }
                else
                {
                    presseedInfoAnimator.Play("Advance");
                }
                if (playerTurn)
                {
                    mainChracter.ToggleCorrect();
                    targetPlayerSkillGauge += 25;
                    if (multiplier != 1)
                    {
                        Debug.Log(200 * (int)(timeMultiplier));
                        Debug.Log(200 * (int)(timeMultiplier) * multiplier);
                    }
                    pointGet = (200 * (int)(timeMultiplier) * multiplier);
                    playerPoint += pointGet;
                }
                else
                {
                    pointGet = (200 * (int)(timeMultiplier));
                    botPoint += pointGet;
                }
            }
            if (checkedResult[0] == "basic")
            {
                if (checkedResult[3] != "collected" && playerTurn)
                {
                    presseedInfoAnimator.Play("Normal_new");
                }
                if (playerTurn)
                {
                    mainChracter.ToggleCorrect();
                    targetPlayerSkillGauge += 10;
                    if (multiplier != 1)
                    {
                        Debug.Log(100 * (int)(timeMultiplier));
                        Debug.Log(100 * (int)(timeMultiplier) * multiplier);
                    }
                    pointGet = (100 * (int)(timeMultiplier) * multiplier);
                    playerPoint += pointGet;
                }
                else
                {
                    pointGet = (100 * (int)(timeMultiplier));
                    botPoint += pointGet;
                }
            }
            target = checkedResult[1];
            wordPressed.text = checkedResult[2];
            wordPressedPoint.text = pointGet.ToString();
            if (checkedResult[0] == "master")
            {
                wordPressedPoint.color = new Color(255f / 255f, 204f / 255f, 109f / 255f);
            }
            else
            {
                wordPressedPoint.color = Color.white;
            }
            if (playerTurn)
            {
                UpdateScore(playerPoint, PlayerScore, previousPlayerScore);
            }
            else
            {
                UpdateScore(botPoint, BotScore, previousBotScore);
            }
            IEnumerator WaitForPicShow()
            {
                pressedInfoUI.SetActive(true);
                gameBoard.setActive(false);
                boardActive = false;
                yield return new WaitForSeconds(3);
                pressedInfoUI.SetActive(false);
                //picPressed = false;
                toggleTurn();
                timer.ResetTimer();
                last.text = target;
                wordText.text = checkedResult[2];
                previousWordImage.sprite = Resources.Load<Sprite>("images/" + selectedImg);
                generateBoard(target);
            }
            StartCoroutine(WaitForPicShow());
            gameBoard.genboardAnimation(timer);
            multiplier = 1;
        }
        //PlayerScore.text = playerPoint.ToString();
        //BotScore.text = botPoint.ToString();
    }

    void checkSkillPoint()
    {
        if (playerSkillGuage >= maxPlayerSkillGauge)
        {
            playerSkillGuage -= maxPlayerSkillGauge;
            targetPlayerSkillGauge -= maxPlayerSkillGauge;
            playerSkillPoint += 1;
        }
        if (playerSkillGuage < targetPlayerSkillGauge)
        {
            playerSkillGuage += (20f * Time.deltaTime);
        }
        //Debug.Log(targetPlayerSkillGauge);
        //Debug.Log(playerSkillGuage);
        //Debug.Log(playerSkillPoint);
    }

    public void ability1()
    {
        Ability1.interactable = false;
        playerSkillPoint -= 2;
        checkedAllResults = images.checkAllResults(cururls, target);
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
        //imageList[index].button.image.color = new Color(119f / 255f, 220f / 255f, 118f / 255f);
        skill1Animator.SetTrigger("activate"+(index+1).ToString());
        skill1Animator.ResetTrigger("idle");
        Debug.Log("Use Ability!");
        enableAbilityButton();
    }

    public void ability2()
    {
        Ability2.interactable = false;
        playerSkillPoint -= 2;
        multiplier = 2;
        skill2Animator.SetTrigger("activate");
        Debug.Log("Use Ability!");
        enableAbilityButton();
    }

    public void ability3()
    {
        playerSkillPoint -= 1;
        playerHealth += 1;
        skill3Animator.SetTrigger("activate");
        Debug.Log("Use Ability!");
        enableAbilityButton();
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
                //if (playerSkillPoint > 0)
                //{
                //Ability1.interactable = true;
                //}
            }
            else
            {
                foreach (var item in imageList)
                {
                    item.button.interactable = false;
                }
                Ability1.interactable = false;
                Ability2.interactable = false;
                timeToAnswer = noobBotV1.CalculateTime();
                if (timeToAnswer > 7.0f)
                {
                    enemy1.ToggleThink();
                    enemy2.ToggleThink();
                }
                botAnswerTier = noobBotV1.CalculateAnswer();
                Debug.Log(timeToAnswer);
                Debug.Log(botAnswerTier);
                StartCoroutine("BotAnswer");

            }
        }
    }

    void UImanager()
    {
        if (playerTurn)
        {
            playerUIanimator.Play("Player_UI_player_turn");
            enemyUIanimator.Play("Enemy_UI_end_turn");
        }
        else
        {
            playerUIanimator.Play("Player_UI_end_turn");
            enemyUIanimator.Play("Enemy_UI_enemy_turn");
        }
    }

    void lifePoint()
    {
        /*if (playerHealth < 1 && PlayerHearts[0] != null)
        {
            gameOver();
            PlayerHearts[0].gameObject.SetActive(false);
        }
        else if (playerHealth < 2 && PlayerHearts[1] != null)
        {
            PlayerHearts[1].gameObject.SetActive(false);
        }
        else if (playerHealth < 3 && PlayerHearts[2] != null)
        {
            PlayerHearts[2].gameObject.SetActive(false);
        }*/
        for (int i = 0; i < PlayerHearts.Count; i++)
        {
            if (i < playerHealth)
            {
                PlayerHearts[i].gameObject.SetActive(true);
            }
            else
            {
                PlayerHearts[i].gameObject.SetActive(false);
            }
        }
        /*if (botHealth < 1 && BotHearts[0] != null)
        {
            gameOver();
            BotHearts[0].gameObject.SetActive(false);
        }
        else if (botHealth < 2 && BotHearts[1] != null)
        {
            BotHearts[1].gameObject.SetActive(false);
        }
        else if (botHealth < 3 && BotHearts[2] != null)
        {
            BotHearts[2].gameObject.SetActive(false);
        }*/
        for (int i = 0; i < BotHearts.Count; i++)
        {
            if (i < botHealth)
            {
                BotHearts[i].gameObject.SetActive(true);
            }
            else
            {
                BotHearts[i].gameObject.SetActive(false);
            }
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
            if (!hasBasic)
            {
                foreach (var item in imageList)
                {
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
            if (!hasAdvance)
            {
                foreach (var item in imageList)
                {
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
            if (!hasMaster)
            {
                Debug.Log("!!!!NO ANSWER!!!!");
            }
        }
        enemy1.ToggleAnswer();
        enemy2.ToggleAnswer();

    }

    public void displayTargetPic()
    {
            targetWord.SetActive(true);

    }

    public void disableTargetPic()
    {
            targetWord.SetActive(false);
    }

    public void enableAbilityButton()
    {
        if (playerTurn && playerSkillPoint > 0)
        {
            if (playerHealth >= 3)
            {
                Ability3.interactable = false;
            }
            else
            {
                Ability3.interactable = true;
            }
        }
        if (playerTurn && playerSkillPoint > 1)
        {
            Ability1.interactable = true;
            Ability2.interactable = true;
        }
        if (playerSkillPoint <= 0)
        {
            Ability3.interactable = false;
        }
        if (playerSkillPoint <= 1)
        {
            Ability1.interactable = false;
            Ability2.interactable = false;
        }
    }

    public void disableAbilityButton()
    {
        Ability1.interactable = false;
        Ability2.interactable = false;
        Ability3.interactable = false;
    }

    public void DisableGameBoard()
    {
        if (boardActive){
            gameBoard.setActive(false);
        }
    }

    public void EnableGameBoard()
    {
        if (boardActive){
            gameBoard.setActive(true);
        }
    }
    private void UpdateScore(int newValue, TextMeshProUGUI text, int numanim_value)
    {
        if (numAnimCoroutine != null)
        {
            StopCoroutine(numAnimCoroutine);
        }
        numAnimCoroutine = StartCoroutine(NumAnimText(newValue, text, numanim_value));
    }

    private IEnumerator NumAnimText(int newValue, TextMeshProUGUI text, int numanim_value)
    {
        WaitForSeconds wait = new WaitForSeconds(1f / numAnimFPS);
        int previousValue = numanim_value;
        int stepAmount;
        if (newValue - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((newValue - previousValue) / (numAnimFPS * numAnimDuration));
        }
        else
        {
            stepAmount = Mathf.CeilToInt((newValue - previousValue) / (numAnimFPS * numAnimDuration));
        }
        if (previousValue < newValue)
        {
            while (previousValue < newValue)
            {
                previousValue += stepAmount;
                if (previousValue > newValue)
                {
                    previousValue = newValue;
                }
                text.SetText(previousValue.ToString("D"));
                yield return wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmount;
                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }
                text.SetText(previousValue.ToString("N0"));
                yield return wait;
            }
        }
    }
}
