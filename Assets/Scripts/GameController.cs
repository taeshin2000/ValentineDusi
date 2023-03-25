using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{

    bool player1Turn = false;
    int player1Point = 0;
    int player1Health = 3;
    int player2Point = 0;
    int player2Health = 3;

    public string selectedWord = "";

    private string target = "ป";

    [SerializeField] Images images;

    [SerializeField] List<Picture> imageList;

    void Start()
    {
        generateBoard(target);
    }

    void Update()
    {

    }

    public void setupImage(List<string> url)
    {
        for (int i = 0; i < 9; i++)
        {
            Debug.Log(i);
            Debug.Log(url[i]);
            imageList[i].imgUrl = url[i];
            Debug.Log(imageList[i].GetType());
            imageList[i].button.image.sprite = Resources.Load<Sprite>("images/" + url[i]);
        }
    }

    void generateBoard(string target)
    {
        var urls = images.generateBoard(target);
        setupImage(urls);
    }

    public void ImageSelected()
    {
        string result = "Hi";
        if (result == "wrong")
        {
            if (player1Turn)
            {
                player1Health -= 1;
            }
            else
            {
                player2Health -= 1;
            }
        }
        else
        {
            if (result == "master")
            {
                if (player1Turn)
                {
                    player1Point += 300;
                }
                else
                {
                    player2Point += 300;
                }
            }
            if (result == "master")
            {
                if (player1Turn)
                {
                    player1Point += 200;
                }
                else
                {
                    player2Point += 200;
                }
            }
            if (result == "master")
            {
                if (player1Turn)
                {
                    player1Point += 100;
                }
                else
                {
                    player2Point += 100;
                }
            }
            toggleTurn();
        }

    }
    void toggleTurn()
    {
        player1Turn = !player1Turn;
    }
}
