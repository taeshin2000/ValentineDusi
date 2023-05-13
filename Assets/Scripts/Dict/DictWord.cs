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

    public void LoadData(GameData gameData){
        this.images = gameData.images;
    }
    public void SaveData(ref GameData gameData){
        gameData.images = this.images;
    }
    void Start()
    {
        DataPersistenceManager.instance.LoadGame();
        fillData();
    }

    void fillData(){
        var isfirst = true;
        foreach (var item in images) {
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
                    temp = 	"<color=red>"+"???"+"</color> ";
                }else{
                    temp = word.word;
                }
                newText.text = temp;
            }
        }
    }
}
