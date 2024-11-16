using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class BuzzwordsManager : MonoBehaviour
{
    //dictionaries to store words and connection values
    private Dictionary<string, int> Modern = new Dictionary<string, int>();
    private Dictionary<string, int> Games = new Dictionary<string, int>();
    private Dictionary<string, int> Computing = new Dictionary<string, int>();
    private Dictionary<string, int> Generic = new Dictionary<string, int>();
    StreamReader ModernReader;
    StreamReader GamesReader;
    StreamReader ComputingReader;
    StreamReader GenericReader;

    private void Start()
    {
        //intialize modern
        ModernReader = new StreamReader("Assets\\TextFiles\\Modern.txt");
        string content = ModernReader.ReadToEnd();
        string[] words = content.Split(",");
        foreach(string word in words)
        {
            Modern.Add(word, -1);
        }
        //intialize games
        GamesReader = new StreamReader("Assets\\TextFiles\\Games.txt");
        content = GamesReader.ReadToEnd();
        words = content.Split(",");
        foreach (string word in words)
        {
            Games.Add(word, -1);
        }
        //intialize comnputing
        ComputingReader = new StreamReader("Assets\\TextFiles\\Computing.txt");
        content = ComputingReader.ReadToEnd();
        words = content.Split(",");
        foreach (string word in words)
        {
            Computing.Add(word, -1);
        }
        //intialize generic
        GenericReader = new StreamReader("Assets\\TextFiles\\Generic.txt");
        content = GenericReader.ReadToEnd();
        words = content.Split(",");
        foreach (string word in words)
        {
            Generic.Add(word, 1);
        }
    }

    //pick random topic
    public string[] PickTopic()
    {
        switch(UnityEngine.Random.Range(0, 3))
        {
            case 0:
                foreach(string word in Modern.Keys)
                {
                    Modern[word] = 3;
                }
                return Modern.Keys.ToArray();
            case 1:
                foreach (string word in Games.Keys)
                {
                    Games[word] = 3;
                }
                return Games.Keys.ToArray();
            case 2:
                foreach (string word in Computing.Keys)
                {
                    Computing[word] = 3;
                }
                return Computing.Keys.ToArray();
        }
        return new string[0];
    }

    //get score
    public int GetScore(string word)
    {
        if(Modern.ContainsKey(word))
        {
            return Modern[word];
        }
        else if (Games.ContainsKey(word))
        {
            return Games[word];
        }
        else if (Computing.ContainsKey(word))
        {
            return Computing[word];
        }
        else if(Generic.ContainsKey(word))
        {
            return Generic[word];
        }
        else
        {
            return 0;
        }
    }
}
