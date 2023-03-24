using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Images : MonoBehaviour

{
    [SerializeField] List<WordImage> images = new List<WordImage>();
    void Start()
    {
        var jsonFile = Resources.Load<TextAsset>("images");
        WordImages imagesInJason = JsonUtility.FromJson<WordImages>(jsonFile.text);
        foreach (WordImage wordImage in imagesInJason.images) {
            images.Add(wordImage);
        }
        var temp = generateBoard("ป");
        foreach (var item in temp)
        {
            Debug.Log(item);
        }
    }
    List<string> generateBoard(string target){
        List<string> output = new List<string>();
        output = addMaster(output,target);
        output = addAdvanced(output,target);
        output = addBasic(output,target);
        output = addRandom(output);
        return output;
    }

    List<string> addMaster(List<string> input,string target){
        List<string> temp = new List<string>();
        foreach (var image in images){
            foreach (var word in image.words){
                if (word.first == target && word.tier == "master" && !input.Contains(image.name)){
                    temp.Add(image.name);
                    break;
                }
            }
        }
        shuffle(temp);
        if (temp.Count >= 1){
            input.Add(temp[0]);
        }
        return input;
    }

    List<string> addAdvanced(List<string> input,string target){
        List<string> temp = new List<string>();
        foreach (var image in images){
            foreach (var word in image.words){
                if (word.first == target && word.tier == "advanced" && !input.Contains(image.name)){
                    temp.Add(image.name);
                    break;
                }
            }
            
        }
        shuffle(temp);
        if (temp.Count >= 1){
            input.Add(temp[0]);
        }
        if (temp.Count >= 2){
            input.Add(temp[1]);
        }
        return input;
    }

    List<string> addBasic(List<string> input,string target){
        List<string> temp = new List<string>();
        foreach (var image in images){
            foreach (var word in image.words){
                if (word.first == target && word.tier == "basic" && !input.Contains(image.name)){
                    temp.Add(image.name);
                    break;
                }
            }
            
        }
        shuffle(temp);
        if (temp.Count >= 1){
            input.Add(temp[0]);
        }
        if (temp.Count >= 2){
            input.Add(temp[1]);
        }
        return input;
    }

    List<string> addRandom(List<string> input){
        List<string> temp = new List<string>();
        foreach (var image in images){
            foreach (var word in image.words){
                if (!input.Contains(image.name)){
                    temp.Add(image.name);
                    break;
                }
            }
            
        }
        shuffle(temp);
        int i = 0;
        while (input.Count < 9){
            input.Add(temp[i]);
            i+=1;
        }
        return input;
    }

    public void shuffle (List<string> list){
        System.Random random = new System.Random();  
        int n = list.Count;  

        for(int i= list.Count - 1; i > 1; i--)
        {
            int rnd = random.Next(i + 1);  

            string value = list[rnd];  
            list[rnd] = list[i];  
            list[i] = value;
        }
    }


}

[System.Serializable]
public class WordImages {
    public WordImage[] images;
}

[System.Serializable]
public class WordImage {
    public string name;
    public Word[] words;
}

[System.Serializable]
public class Word {
    public string word;
    public string first;
    public string last;
    public string tier;
}