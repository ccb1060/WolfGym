using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasRandomizer : MonoBehaviour
{
    [SerializeField] private TMP_Text _connections;
    [SerializeField] private TMP_Text _likes;
    [SerializeField] private TMP_Text _comments;
    [SerializeField] private TMP_Text _pop_hashtags;

    public int connections { get; private set; } = 0;

    public void IncreaseConnections(int newConnections)
    {
        connections += newConnections;

        float likes = connections;
        float comments = connections;
        float pop = connections;

        _connections.text = FormatBigNumbers(connections);
        _likes.text = FormatBigNumbers(connections);
        _comments.text = FormatBigNumbers(connections);
        _pop_hashtags.text = pop + "M New Posts!";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private string FormatBigNumbers(float num)
    {
        string character = "";

        num /= 1000;

        if (num < 1)
        {

        }
        else if (num > 1) 
        { 
        
        }



        
        string value = (num %= 1000).ToString();

        return value + character;
    }
}
