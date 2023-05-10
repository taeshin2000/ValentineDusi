using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WordImages
{
    public List<WordImage> images;
    //default value when there's no data to load
    public WordImages(TextAsset json) {
        this.images = new List<WordImage>();
        WordImages imagesInJason = JsonUtility.FromJson<WordImages>(json.text);
        foreach (WordImage wordImage in imagesInJason.images)
        {
            images.Add(wordImage);
        }
    }

}

[System.Serializable]
public class WordImage
{
    public string name;
    public Word[] words;
}

[System.Serializable]
public class Word
{
    public string word;
    public string first;
    public string last;
    public string tier;
    public string status;
}
