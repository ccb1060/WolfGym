using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.IO;

public class NewsManager : MonoBehaviour
{
    //Allow retrieval of random generic/topical buzzwords
    [SerializeField] BuzzwordsManager BuzzwordsManager;

    //The blueprint for the article
    [SerializeField] GameObject articlePrefab;
    //The current prompt being displayed to the player
    NewsController prompt;

    //All possible articles
    string[] articleList;

    //the current post being worked on
    string[] post;

    string input = "";

    // Start is called before the first frame update
    void Start()
    {
        

        //Create the article and move it to the correct position
        prompt = Instantiate(articlePrefab).GetComponent<NewsController>();
        prompt.gameObject.transform.position = new Vector3(-6,1,0);

        //Put the articles from the text file into the array
        StreamReader reader = new StreamReader(Application.dataPath+"/TextFiles/Articles.txt");
        string contents = reader.ReadToEnd();
        reader.Close();
        articleList = contents.Split('|');
    }

    public void changeArticle()
    {
        //Picks a random article from the list, rerolling if it gets the same one
        do
        {
            post = articleList[Random.Range(1, articleList.Length - 1)].Split(';');
        }
        while (post[0].Equals(prompt.headText.text));

        prompt.headText.text = post[0];
        updatePost(post, "");
        
    }

    void updatePost(string[] text, string input)
    {
        string[] bodyParts = text[1].Split('_');
        string bodyWhole = bodyParts[0];
        for (int i = 1; i < bodyParts.Length; i++)
        {
            if(input == "")
                bodyWhole += "<color=red>_</color>" + bodyParts[i];
            else
            {
                bodyWhole += "<color=purple>" + input + "</color>"+bodyParts[i];
            }
        }
        prompt.bodyText.text = bodyWhole;
        text[1] = bodyWhole;
    }
}
