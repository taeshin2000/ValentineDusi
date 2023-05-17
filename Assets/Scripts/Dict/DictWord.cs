using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DictWord : MonoBehaviour,IDataPersistence
{
    public VerticalLayoutGroup firstCol;
    public VerticalLayoutGroup secondCol;

    [SerializeField] List<WordImage> images = new List<WordImage>();
    [SerializeField] GameObject imageWordPrefabs;
    [SerializeField] TextMeshProUGUI newwordPrefabs;
    [SerializeField] TextMeshProUGUI collectedDisplay;

    [SerializeField] Button previousButton;
    [SerializeField] Button nextButton;

    public int currentPage = 1;
    private int maxPage;
    private int allword = 0;
    private int collectedWord = 0;

    public void LoadData(GameData gameData){
        this.images = gameData.images;
    }
    public void SaveData(ref GameData gameData){
        gameData.images = this.images;
    }
    void Start()
    {
        previousButton.image.gameObject.SetActive(false);
        DataPersistenceManager.instance.LoadGame();
        maxPage = (int) Mathf.Ceil(images.Count/6f);
        fillData();
        foreach (var item in images)
        {
            foreach (var word in item.words)
            {
                allword += 1;
                if (word.status == "collected"){
                    collectedWord += 1;
                }
            }
        }
        collectedDisplay.text = collectedWord.ToString()+"/"+allword.ToString();
    }

    void fillData(){
        while (firstCol.transform.childCount > 0) {
            DestroyImmediate(firstCol.transform.GetChild(0).gameObject);
        }
        while (secondCol.transform.childCount > 0) {
            DestroyImmediate(secondCol.transform.GetChild(0).gameObject);
        }
        var isfirst = true;
        int index = (currentPage-1)*6;
        int range = 6;
        if (currentPage == maxPage){
            range = images.Count%6;
        }
        foreach (var item in images.GetRange(index,range)) {
            GameObject newImage = Instantiate(imageWordPrefabs);
            if (isfirst) {
                newImage.transform.SetParent(firstCol.transform);
            }else{
                newImage.transform.SetParent(secondCol.transform);
            }
            isfirst = !isfirst;
            var imageChild = newImage.GetComponentInChildren<Image>();  
            imageChild.sprite =  Resources.Load<Sprite>("images/" + item.name);
            var wordListChild = newImage.GetComponentInChildren<VerticalLayoutGroup>();   
            foreach (var word in item.words)
            {
                TextMeshProUGUI newText = Instantiate(newwordPrefabs,wordListChild.transform);
                string temp;
                if (word.status == "unknown"){
                    temp = 	"???";
                }else if(word.status == "found"){
                    temp = word.word;
                    newText.color = new Color(150f/255f,150f/255f,150f/255f);
                }else{
                    temp = word.word;
                }
                newText.text = temp;
            }
        }
    }

    public void nextPage(){
        AudioManager.instance.Play("DictPage");
        if (currentPage!=maxPage){
            currentPage += 1;
            if (currentPage == 1){
                previousButton.image.gameObject.SetActive(false);
            }else{
                previousButton.image.gameObject.SetActive(true);
            }

            if (currentPage == maxPage){
                nextButton.image.gameObject.SetActive(false);
            }else{
                nextButton.image.gameObject.SetActive(true);
            }
            fillData();
        }
    }

    public void previousPage(){
        AudioManager.instance.Play("DictPage");
        if (currentPage!=1){
            currentPage -= 1;
            if (currentPage == 1){
                previousButton.image.gameObject.SetActive(false);
            }else{
                previousButton.image.gameObject.SetActive(true);
            }
            
            if (currentPage == maxPage){
                nextButton.image.gameObject.SetActive(false);
            }else{
                nextButton.image.gameObject.SetActive(true);
            }
            fillData();
        }
    }
}
