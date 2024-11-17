using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.IO;
using TMPro;
using UnityEngine.Windows;
using Unity.Collections.LowLevel.Unsafe;

public class NewsManager : MonoBehaviour
{
    //Allow retrieval of random generic/topical buzzwords
    [SerializeField] BuzzwordsManager buzzwordsManager;

    [SerializeField] GameManager gameManager;


    //The field that displays the post
    [SerializeField] TMP_Text postText;

    //The text that displays the hashtag label
    [SerializeField] TMP_Text hastagHead;

    //The field that displays the hastags
    [SerializeField] TMP_Text hastags;

    //The field that displays the notes
    [SerializeField] TMP_Text notepaper;

    [SerializeField] CanvasRandomizer canvas;

    //All possible articles
    private string[] articleList;

    public string[] ArticleList
    {
        get { return articleList; }
    }


    //the current post being worked on
    string[] post;

    // Start is called before the first frame update
    void Start()
    {   
        //Put the articles from the text file into the array
        StreamReader reader = new StreamReader(Application.dataPath+"/TextFiles/Articles.txt");
        string contents = reader.ReadToEnd();
        reader.Close();
        articleList = contents.Split('|');

        post = articleList[0].Split(';');
        updatePost("");
    }

    public void changeArticle()
    {
        post = articleList[Random.Range(3, articleList.Length - 1)].Split(';');

        buzzwordsManager.PickTopic();
        canvas.SetNotes();
        canvas.SetQuota(gameManager.quota);
        List<string> words = new List<string>();
        while(words.Count < 5) 
        {
            string word = buzzwordsManager.CurrentTopic[UnityEngine.Random.Range(0, buzzwordsManager.CurrentTopic.Count - 1)].word;
            if(!words.Contains(word))
            {
                words.Add(word);
            }
        }
        canvas.SetHashtags(words);
        updatePost("");
    }

    public void updatePost(string input)
    {
        string[] bodyParts = post[1].Split('_');
        string bodyWhole = bodyParts[0];
        for (int i = 1; i < bodyParts.Length; i++)
        {
            if(input == "")
                bodyWhole += "<color=red>_</color>" + bodyParts[i];
            else
            {
                gameManager.playerScore += buzzwordsManager.GetScore(input);
                canvas.IncreaseConnections(buzzwordsManager.GetScore(input));
                buzzwordsManager.Discover(input);
                bodyWhole += "<color=purple>" + input + "</color>"+bodyParts[i];
                input = "";
                gameManager.PlaySound(2);
            }
        }
        postText.text = bodyWhole;
        post[1] = bodyWhole;
    }

    /// <summary>
    /// Called after the player misses three articles
    /// </summary>
    public void EndOfGamePost()
    {
        post = articleList[1].Split(';');

        notepaper.text = articleList[2].Split(';')[1];

        notepaper.fontSize = 16;

        hastagHead.text = "YOU'RE FIRED!";
        hastags.text = "#BetterLuckNextTime #PackYourStuff #Ouch";
        updatePost("");
    }
}
