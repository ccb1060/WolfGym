using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CanvasRandomizer : MonoBehaviour
{
    [SerializeField] private TMP_Text _connections;
    [SerializeField] private TMP_Text _likes;
    [SerializeField] private TMP_Text _comments;
    [SerializeField] private TMP_Text _pop_hashtags;
    [SerializeField] private TMP_Text topichashtags;
    [SerializeField] private TMP_Text topicwords;
    [SerializeField] private TMP_Text genericwords;
    [SerializeField] private TMP_Text quota;

    [SerializeField] private BuzzwordsManager buzzwordsManager;

    public int connections { get; private set; } = 0;

    void Start()
    {
        _connections.text = "Trending Hashtags:";
        _likes.text = "0";
        _comments.text = "0";
    }

    /// <summary>
    /// Formats and randomizes a collection of meaningless number values on screen.
    /// Should be called every time the player gains new connections.
    /// </summary>
    /// <param name="newConnections"></param>
    public void IncreaseConnections(int newConnections)
    {
        connections += newConnections;

        float likes = connections * Random.Range(0.4f, 0.8f);
        float comments = connections * Random.Range(0.05f, 0.5f);

        _connections.text = FormatBigNumbers(connections) + " Connections are Posting";
        _likes.text = FormatBigNumbers(likes);
        _comments.text = FormatBigNumbers(comments);
    }

    /// <summary>
    /// Turns really big numbers into equivalent truncated strings
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private string FormatBigNumbers(float num)
    {
        string character = "";
        string value = num.ToString("0");

        if (num >= 1000000000) // Billion
        {
            character = "B";
            value = (num / 1000000000).ToString("0.0");
        }
        else if (num >= 1000000) // Million
        {
            character = "M";
            value = (num / 1000000).ToString("0.0");
        }
        else if (num >= 1000) // Thousand
        {
            character = "K";
            value = (num / 1000).ToString("0.0");
        }

        return value + character;
    }

    public void SetHashtags(List<string> words)
    {
        topichashtags.text = "";
        topichashtags.text = "#" + words[0].Trim() + " #" + words[1].Trim() + " #" + words[2].Trim() + " #" + words[3].Trim() + " #" + words[4].Trim();
    }

    public void SetNotes()
    {
        topicwords.text = "";
        genericwords.text = "";
        foreach(WordClass word in buzzwordsManager.CurrentTopic)
        {
            if(word.discovered)
            {
                topicwords.text += word.word.Trim() + "  ";
            }
        }
        foreach(WordClass word in buzzwordsManager.Generic)
        {
            if(word.discovered)
            {
                genericwords.text += word.word.Trim() + "  ";
            }
        }
    }

    public void SetQuota(int quotaNum)
    {
        quota.text = "Quota: " + quotaNum.ToString();
    }

    /// <summary>
    /// A temporary button event to quit the game. Can be moved to whereever
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
