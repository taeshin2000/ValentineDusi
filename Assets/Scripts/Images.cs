using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Images : MonoBehaviour,IDataPersistence

{
    [SerializeField] List<WordImage> images = new List<WordImage>();
    void Start(){
        DataPersistenceManager.instance.LoadGame();
    }
    public void LoadData(WordImages wordImages){
        this.images = wordImages.images;
    }
    public void SaveData(ref WordImages wordImages){
        wordImages.images = this.images;
    }

    public class wordBoard{
        public string name;
        public string tier;
        public string status;

        public wordBoard(string n,string t, string s){
            name = n;
            tier = t;
            status = s;
        }
    }

    public List<wordBoard> generateBoard(string target)
    {
        List<wordBoard> output = new List<wordBoard>();
        output = addMaster(output, target);
        output = addAdvanced(output, target);
        output = addBasic(output, target);
        output = addRandom(output);
        shuffle(output);
        Debug.Log(output.Count);
        return output;
    }

    List<wordBoard> addMaster(List<wordBoard> input, string target)
    {
        List<wordBoard> temp = new List<wordBoard>();
        foreach (var image in images)
        {
            var added = false;
            foreach (var word in image.words)
            {   
                if (added){
                    added = false;
                    break;
                }
                var canAdd = true;
                foreach (var item in input) {
                    if (item.name == image.name){
                        canAdd = false;
                    }
                }
                if (word.first == target && word.tier == "master" && canAdd){
                    temp.Add(new wordBoard(image.name,word.tier,word.status));
                    added = true;
                    break;
                }
                
            }

        }
        shuffle(temp);
        if (temp.Count >= 1)
        {
            input.Add(temp[0]);
        }
        return input;
    }

    List<wordBoard> addAdvanced(List<wordBoard> input, string target)
    {
        List<wordBoard> temp = new List<wordBoard>();
        foreach (var image in images)
        {
            var added = false;
            foreach (var word in image.words)
            {   
                if (added){
                    break;
                }
                var canAdd = true;
                foreach (var item in input) {
                    if (item.name == image.name){
                        canAdd = false;
                    }
                }
                if (word.first == target && word.tier == "advanced" && canAdd){
                    temp.Add(new wordBoard(image.name,word.tier,word.status));
                    added = true;
                    break;
                }
            }
        }
        shuffle(temp);
        if (temp.Count >= 1)
        {
            input.Add(temp[0]);
        }
        if (temp.Count >= 2)
        {
            input.Add(temp[1]);
        }
        return input;
    }

    List<wordBoard> addBasic(List<wordBoard> input, string target)
    {
         List<wordBoard> temp = new List<wordBoard>();
        foreach (var image in images)
        {
            var added = false;
            foreach (var word in image.words)
            {   
                if (added){
                    break;
                }
                var canAdd = true;
                foreach (var item in input) {
                    if (item.name == image.name){
                        canAdd = false;
                    }
                }
                if (word.first == target && word.tier == "basic" && canAdd){
                    temp.Add(new wordBoard(image.name,word.tier,word.status));
                    added = true;
                    break;
                }
            }

        }
        shuffle(temp);
        if (temp.Count >= 1)
        {
            input.Add(temp[0]);

        }
        if (temp.Count >= 2)
        {
            input.Add(temp[1]);
        }
        return input;
    }

    List<wordBoard> addRandom(List<wordBoard> input)
    {
        List<wordBoard> temp = new List<wordBoard>();
        foreach (var image in images)
        {
            var added = false;
            foreach (var word in image.words)
            {   
                if (added){
                    break;
                }
                var canAdd = true;
                foreach (var item in input) {
                    if (item.name == image.name){
                        canAdd = false;
                    }
                }
                if (canAdd){
                    temp.Add(new wordBoard(image.name,"random",word.status));
                    added = true;
                    break;
                }
            }

        }
        shuffle(temp);
        int i = 0;
        while (input.Count < 9)
        {
            input.Add(temp[i]);
            i += 1;
        }
        return input;
    }

    public void shuffle(List<wordBoard> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;

        for (int i = list.Count - 1; i > 1; i--)
        {
            int rnd = random.Next(i + 1);

            wordBoard value = list[rnd];
            list[rnd] = list[i];
            list[i] = value;
        }
    }

    public List<string> checkResult(string name, string target,bool playerTurn)
    {
        DataPersistenceManager.instance.LoadGame();
        List<string> output = new List<string>();
        foreach (var image in images)
        {
            if (image.name == name)
            {
                foreach (var word in image.words)
                {
                    if (word.first == target)
                    {
                        output.Add(word.tier);
                        output.Add(word.last);
                        output.Add(word.word);
                        if (word.status == "unknown"){
                            if (playerTurn){
                                word.status = "collected";
                            }else{
                                word.status = "found";
                            }
                            DataPersistenceManager.instance.SaveGame();

                        }
                        return output;
                    }
                }
            }
        }
        output.Add("failed");
        output.Add("");
        output.Add("");

        return output;
    }

    public List<string> checkAllResults(List<string> urls, string target)
    {
        List<string> output = new List<string>();
        int found = 0;
        foreach (var name in urls)
        {
            foreach (var image in images)
            {
                if (image.name == name)
                {
                    found = 0;
                    foreach (var word in image.words)
                    {
                        if (word.first == target)
                        {
                            output.Add(word.tier);
                            found = 1;
                            break;
                        }
                    }
                    if (found == 0)
                    {
                        output.Add("failed");
                    }
                }
            }
        }
        return output;
    }

    public WordImage randomWord()
    {
        WordImage tempwordimg = new WordImage();
        int tempindex = Random.Range(0,images.Count);
        List<Word> temp = new List<Word>();
        foreach (var word in images[tempindex].words)
        {
            temp.Add(word);
        }
        Word[] tempWordList = new Word[1];
        tempWordList[0] = temp[Random.Range(0, temp.Count)];
        tempwordimg = new WordImage {name = images[tempindex].name, words = tempWordList};
        return tempwordimg;
    }
}
