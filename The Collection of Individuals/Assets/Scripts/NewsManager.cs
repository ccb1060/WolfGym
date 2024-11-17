using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.IO;
using TMPro;
using UnityEngine.Windows;

public class NewsManager : MonoBehaviour
{
    //Allow retrieval of random generic/topical buzzwords
    [SerializeField] BuzzwordsManager buzzwordsManager;

    [SerializeField] GameManager gameManager;


    //The field that displays the text
    [SerializeField] TMP_Text postText;

    [SerializeField] CanvasRandomizer canvas;

    //All possible articles
    private string[] articleList;

    public string[] ArticleList
    {
        get { return articleList; }
    }

    List<string> inputWords = new List<string>();

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
        //Picks a random article from the list, rerolling if it gets the same one
        foreach(string word in inputWords)
        {
            gameManager.playerScore += buzzwordsManager.GetScore(word);
            canvas.IncreaseConnections(buzzwordsManager.GetScore(word));
            buzzwordsManager.Discover(word);
        }
        inputWords.Clear();
        post = articleList[Random.Range(1, articleList.Length - 1)].Split(';');
        buzzwordsManager.PickTopic();
        canvas.SetNotes();
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
                inputWords.Add(input);
                bodyWhole += "<color=purple>" + input + "</color>"+bodyParts[i];
                input = "";
            }
        }
        postText.text = bodyWhole;
        post[1] = bodyWhole;
    }
}
