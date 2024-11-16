using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.IO;

public class NewsManager : MonoBehaviour
{
    //The blueprint for the article
    [SerializeField] GameObject articlePrefab;
    //The current article being displayed to the player
    NewsController article;
    //How long the player has until the article changes
    [SerializeField]float timeUntilChange = 0;

    [SerializeField] GameObject progressBar;
    float maxtime = 0;

    //The player's current score
    [SerializeField] int playerScore = 0;

    //All possible articles
    string[] articleList;

    // Start is called before the first frame update
    void Start()
    {
        maxtime = 5;
        timeUntilChange = 5;

        //Create the article and move it to the correct position
        article = Instantiate(articlePrefab).GetComponent<NewsController>();
        article.gameObject.transform.position = new Vector3(-6,1,0);

        //Put the articles from the text file into the array
        StreamReader reader = new StreamReader(Application.dataPath+"/Articles/Articles.txt");
        string contents = reader.ReadToEnd();
        reader.Close();
        articleList = contents.Split('|');
    }

    // Update is called once per frame
    void Update()
    {
        if(timeUntilChange < 0)
        {
            changeArticle();
            maxtime = 5 - playerScore/10;
            timeUntilChange = maxtime;
        }

        progressBar.GetComponent<UnityEngine.UI.Image>().fillAmount = timeUntilChange/maxtime;
        progressBar.GetComponent<UnityEngine.UI.Image>().color = new Color( (1 - timeUntilChange / maxtime), 255, 0);
        timeUntilChange -= Time.deltaTime;

    }

    void changeArticle()
    {
        //Picks a random article from the list, rerolling if it gets the same one
        string[] text;
        do
        {
            text = articleList[Random.Range(0, articleList.Length - 1)].Split(';');
        }
        while (text[0].Equals(article.headText.text));
        
        article.headText.text = text[0];
        article.bodyText.text = text[1]; 
    }
}
