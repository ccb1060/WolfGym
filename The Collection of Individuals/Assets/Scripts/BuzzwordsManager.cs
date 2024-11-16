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
    private Dictionary<string, int> Topical = new Dictionary<string, int>();
    private Dictionary<string, int> Generic = new Dictionary<string, int>();
    StreamReader TopicalReader;
    StreamReader GenericReader;

    private void Start()
    {
        //intialize topical
        TopicalReader = new StreamReader("Assets\\TextFiles\\Topical.txt");
        string topicalContent = TopicalReader.ReadToEnd();
        string[] topicalWords = topicalContent.Split(",");
        foreach(string word in topicalWords)
        {
            Topical.Add(word, -1);
        }
        //intialize generic
        GenericReader = new StreamReader("Assets\\TextFiles\\Generic.txt");
        string genericContent = GenericReader.ReadToEnd();
        string[] genericWords = genericContent.Split(",");
        foreach (string word in genericWords)
        {
            Generic.Add(word, 1);
        }
    }

    //reset topical scores
    public void ResetTopical()
    {
        for(int i = 0; i < Topical.Keys.Count; i++)
        {
            string temp = Topical.Keys.ToArray()[i];
            Topical[temp] = -1;
        }
    }

    //pick random topical
    public string PickTopical()
    {
        string temp = "";
        temp = Topical.Keys.ToArray()[(int)UnityEngine.Random.Range(0, Topical.Keys.Count - 1)];
        Topical[temp] = 3;
        return temp;
    }

    //get score
    public int GetScore(string word)
    {
        if(Topical.ContainsKey(word))
        {
            return Topical[word];
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
