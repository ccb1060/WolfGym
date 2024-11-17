using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    List<AudioSource> sources = new List<AudioSource>();

    [SerializeField] List<AudioClip> audios;

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
        maxtime = 30;
        timeUntilChange = maxtime;
        inTutorial = false;

        //When the player inputs a string, update the post
        field.onEndEdit.AddListener(delegate 
        { 
            postManager.updatePost(field.text);
            field.text = "";
        });

        //Creates a new audio source and adds its corresponding clip
        for (int i = 0; i < audios.Count; i++)
        {
            sources.Add(gameObject.AddComponent<AudioSource>());
            sources[i].clip = audios[i];
        }
        
        

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!sources[0].isPlaying)
        {
            PlaySound(0);
        }
        //If the player has run out of time, change the post
        if (timeUntilChange < 0)
        {
            postManager.changeArticle();
            maxtime -= playerScore / 10;
            timeUntilChange = maxtime;

            ArticleMissed();
            sources[0].Play();
            sources[0].volume = 1;
        }
        else if (timeUntilChange < 10)
        {
            if (!sources[5].isPlaying)
            {
                
                PlaySound(5);
            }

            //The music fades out as the warning fades in
            sources[0].volume = timeUntilChange/10;
            sources[5].volume = 1- timeUntilChange / 10;

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
        PlaySound(3);

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
    public void ArticleSuccess()
    {
        if (true)
        {
            postManager.changeArticle();
            maxtime = 5 - playerScore / 10;
            timeUntilChange = maxtime;
        }
    }
    /// <summary>
    /// Plays the corresponding audio
    /// </summary>
    /// <param name="index"> The index of the sound you want to play, indices are as follows:
    /// 0 - Music
    /// 1 - Non-buzzword scored
    /// 2 - Player Inputted word(small pop noise)
    /// 3 - Player recieved an X for not being fast enough
    /// 4 - Generic buzzword scored
    /// 5 - Player is running out of time(less than three seconds till change)
    /// </param>
    public void PlaySound(int index)
    {
        sources[index].Play();
    }
}
