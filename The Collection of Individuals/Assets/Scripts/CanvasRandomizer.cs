using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CanvasRandomizer : MonoBehaviour
{
    [SerializeField] private TMP_Text _connections;
    [SerializeField] private TMP_Text _likes;
    [SerializeField] private TMP_Text _comments;
    [SerializeField] private TMP_Text _pop_hashtags;

    public int connections { get; private set; } = 0;

    void Start()
    {
        _connections.text = "Virtual Content Marketing Response intern @ BigMedia\n" +
                            "Connections: 0";
        _likes.text = "0";
        _comments.text = "0";
        _pop_hashtags.text = Random.Range(2.0f, 50.0f).ToString("0.0") + "M New Posts!";
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
        float pop = Random.Range(2.0f, 50.0f);

        _connections.text = "Media Consultant Intern @ BigMedia\n" +
                            "Connections: " + FormatBigNumbers(connections);

        _likes.text = FormatBigNumbers(likes);
        _comments.text = FormatBigNumbers(comments);
        _pop_hashtags.text = pop.ToString("0.0") + "M New Posts!";
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

    /// <summary>
    /// A temporary button event to quit the game. Can be moved to whereever
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
