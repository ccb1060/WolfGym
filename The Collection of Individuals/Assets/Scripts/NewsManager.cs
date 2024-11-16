using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class NewsManager : MonoBehaviour
{
    [SerializeField] GameObject articlePrefab;
    NewsController article;
    [SerializeField]float timeUntilChange = 0;
    [SerializeField] int playerScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        timeUntilChange = 5;
        article = Instantiate(articlePrefab).GetComponent<NewsController>();
        article.gameObject.transform.position = new Vector3(-6,1,0);
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
        article.UpdateText(playerScore.ToString());
    }
}
