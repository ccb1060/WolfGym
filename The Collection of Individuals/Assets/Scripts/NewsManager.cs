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
        //StreamReader reader = new StreamReader(Application.dataPath+"/TextFiles/Articles.txt");
        //This was done because Unity builds hate txt files
        string contents = "Welcome to Sycophants Incorporated!;Welcome to Sycophants Incorporated! You are our new Social Media Virtual Content Marketing Response intern, whose job is to gain as many connections on Connect'In as possible! Our advanced AI post generation software will provide you with the basic outline of a post, all you need to do is figure out which buzzwords are trending and fill in the blanks! If you get them right, they'll automatically be added to your notepad on the right, so be sure to check that frequently. While we try to be forgiving, working in such a dynamic, fast-paced, environment requires our employees to go above and beyond. As such, if you fail to post about a trending topic three times, you will be forcibly promoted to customer. Press 'Post' to get started, and good Luck!|\r\n;[INSERT INTERN NAME HERE], we received an overwhelming response from our internship positions, which makes us feel both humble and proud that so many talented individuals have had the chance to work with us. We regret to inform you that we have decided to continue forward with other candidates in your position at this time. Stay Positive! #lmao|\r\n;CREDITS\r\n\r\nLow on Time indicator - Outro Bass Pulse by day_tripper13\r\nhttps://freesound.org/people/day_tripper13/sounds/117171/\r\n\r\nFailed to Post - Error #3 by BloodPixelHero\r\nhttps://freesound.org/people/BloodPixelHero/sounds/572938/\r\n\r\nInputted word - Pop_10.aif by SunnySideSound\r\nhttps://freesound.org/people/SunnySideSound/sounds/67087/ \r\n\r\nMusic - 8bitbattle01 by moodytail\r\nhttps://moodytail.itch.io/moodys-free-music-pack |\r\nToday's meeting was a game-changer!;We focused on leveraging _ to enable _ through strategic _. This aligns perfectly with our vision of _. By adopting _ frameworks, we're accelerating our ability to disrupt _ and maximize _ for our stakeholders.|\r\nEmbracing the Future of Work;As we scale our operations, it's critical to empower our teams with _ tools and integrate _ practices. This allows us to unlock new value and optimize _ at every level. Together, we're driving _ and pushing boundaries in _.|\r\nExciting News in the World of Innovation!;We've just launched our _ initiative aimed at enhancing _ with cutting-edge _. This project is poised to disrupt the _ industry and create a _ advantage for early adopters. Through our partnership with _, we are reimagining what's possible in the _ space.|\r\nThe Path to Sustainable Growth;After months of _, we are proud to announce our latest strategy for _. By focusing on _ and implementing _ methodologies, we are primed to experience exponential growth and _ results. This is just the beginning as we continue to optimize _ across all channels.|\r\n Key Insights from Our Latest Summit;We're excited to reveal the takeaways from our _ summit, where thought leaders from around the globe gathered to discuss the future of _. The consensus? _ will be the cornerstone of success in the coming years, and companies must adopt _ to stay ahead of the curve. Let's drive _ forward together!|"
        //reader.Close();
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
