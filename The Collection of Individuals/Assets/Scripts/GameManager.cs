using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    //The post the player is working on
    [SerializeField] NewsManager postManager;

    //The input field for the player
    [SerializeField] TMP_InputField field; 

    //The bar showing how long the player has until the next prompt is generated
    [SerializeField] GameObject progressBar;

    // The list of three xs that determine the end of the game
    [SerializeField] List<Image> xValues;

    //The amount of time the player had to complete their post the moment it appeared 
    float maxtime = 0;

    //How long the player currently has until the article changes
    [SerializeField] float timeUntilChange = 0;

    //The player's current score
    [SerializeField] public int playerScore = 0;

    //Buzzword manager
    [SerializeField] BuzzwordsManager buzzwordsManager;

    private int numFailures = 0; 

    private bool inTutorial;

    
    // Start is called before the first frame update
    void Start()
    {
        maxtime = 5;
        timeUntilChange = 5;
        inTutorial = false;

        field.onEndEdit.AddListener(delegate 
        { 
            postManager.updatePost(field.text);
            field.text = "";
        });
    }

    // Update is called once per frame
    void Update()
    {
        //If the player has run out of time, change the post
        if (timeUntilChange < 0)
        {
            postManager.changeArticle();
            maxtime = 5 - playerScore / 10;
            timeUntilChange = maxtime;

            ArticleMissed();
        }

        //Changes the width and color of the progress bar in relation to how long the player has to post
        progressBar.GetComponent<UnityEngine.UI.Image>().fillAmount = timeUntilChange / maxtime;
        progressBar.GetComponent<UnityEngine.UI.Image>().color = new Color((1 - timeUntilChange / maxtime), 255, 0);

        if(!inTutorial)
            timeUntilChange -= Time.deltaTime;
    }

    /// <summary>
    /// The player failed to madlibs the article, and is punished
    /// </summary>
    private void ArticleMissed()
    {
        // Changes the display of another X to be red
        xValues[numFailures++].color = Color.HSVToRGB(0, 84, 78);

        // Once the player misses three articles, the game ends
        if (numFailures >= 3)
        {
            postManager.EndOfGamePost();
            inTutorial = true;
        }
    }

    /// <summary>
    /// BUTTON EVENT - The player successfully prolonged their inevitable
    /// </summary>
    private void ArticleSuccess()
    {
        if (true)
        {
            postManager.changeArticle();
            maxtime = 5 - playerScore / 10;
            timeUntilChange = maxtime;
        }
    }
}
