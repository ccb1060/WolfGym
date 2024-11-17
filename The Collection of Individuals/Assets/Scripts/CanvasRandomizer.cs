using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CanvasRandomizer : MonoBehaviour
{
    [SerializeField] private TMP_Text _connections;
    [SerializeField] private TMP_Text _likes;
    [SerializeField] private TMP_Text _comments;

    public int connections { get; private set; } = 0;

    void Start()
    {
        _connections.text = "People are Posting:";
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

        _connections.text = FormatBigNumbers(connections) + " Connections are Posting:";
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

    /// <summary>
    /// A temporary button event to quit the game. Can be moved to whereever
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Restarts the scene once the player hits gameover
    /// </summary>
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
