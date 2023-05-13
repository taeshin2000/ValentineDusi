using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<WordImage> images;
    public List<Level> levels;

    //default value when there's no data to load
    public GameData(TextAsset json) {
        GameData DataInJason = JsonUtility.FromJson<GameData>(json.text);
        this.levels = new List<Level>();
        foreach (Level level in DataInJason.levels)
        {
            levels.Add(level);
        }
        this.images = new List<WordImage>();
        foreach (WordImage wordImage in DataInJason.images)
        {
            images.Add(wordImage);
        }
    }
}
[System.Serializable]
public class Levels{
    public List<Level> levels;
}
[System.Serializable]
public class Level{
    public bool clear;
    public int highScore;
}

[System.Serializable]
public class WordImages
{
    public List<WordImage> images;
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
