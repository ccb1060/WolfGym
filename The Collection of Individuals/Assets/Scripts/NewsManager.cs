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

    //The player's current score
    [SerializeField] int playerScore = 0;

    //All possible articles
    string[] articleList;

    // Start is called before the first frame update
    void Start()
    {
        timeUntilChange = 5;

        //Create the article and move it to the correct position
        article = Instantiate(articlePrefab).GetComponent<NewsController>();
        article.gameObject.transform.position = new Vector3(-6,1,0);

        StreamReader reader = new StreamReader(Application.dataPath+"/Articles/Articles.txt");
        string contents = reader.ReadToEnd();
        reader.Close();

        articleList = contents.Split(',');
        

    }

    // Update is called once per frame
    void Update()
    {
        if(timeUntilChange < 0)
        {
            changeArticle();
            timeUntilChange = 5 - playerScore/10;
        }

        timeUntilChange -= Time.deltaTime;

    }

    void changeArticle()
    {
        playerScore = playerScore + 1;
        article.UpdateText(articleList[Random.Range(0, articleList.Length - 1)]);
        
    }
}
