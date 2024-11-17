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
    private List<List<WordClass>> Lists = new List<List<WordClass>>();
    private List<WordClass> Business = new List<WordClass>();
    private List<WordClass> Education = new List<WordClass>();
    private List<WordClass> EnvironmentList = new List<WordClass>();
    private List<WordClass> Finance = new List<WordClass>();
    private List<WordClass> Games = new List<WordClass>();
    private List<WordClass> Health = new List<WordClass>();
    private List<WordClass> Marketing = new List<WordClass>();
    private List<WordClass> Technology = new List<WordClass>();
    public List<WordClass> Generic = new List<WordClass>();
    public List<WordClass> CurrentTopic = new List<WordClass>();
    StreamReader ListReader;
    public int topicValue = 3;
    public int genericValue = 1;
    public int irrelevantValue = -1;

    private void Start()
    {
        //intialize list of lists
        Lists.Add(Business);
        Lists.Add(Education);
        Lists.Add(EnvironmentList);
        Lists.Add(Finance);
        Lists.Add(Games);
        Lists.Add(Health);
        Lists.Add(Marketing);
        Lists.Add(Technology);
        Lists.Add(Generic);

        //intialize Business
        ListReader = new StreamReader(Application.dataPath + "/Resources/Business.txt");
        string content = content = ListReader.ReadToEnd();
        string[] words = content.Split(",");
        foreach(string word in words)
        {
            Business.Add(new WordClass(word, topicValue));
        }

        //intialize Education
        ListReader = new StreamReader(Application.dataPath + "/Resources/Education.txt");
        content = ListReader.ReadToEnd();
        words = content.Split(",");
        foreach (string word in words)
        {
            Education.Add(new WordClass(word, topicValue));
        }

        //intialize Environment
        ListReader = new StreamReader(Application.dataPath + "/Resources/Environment.txt");
        content = ListReader.ReadToEnd();
        words = content.Split(",");
        foreach (string word in words)
        {
            EnvironmentList.Add(new WordClass(word, topicValue));
        }

        //intialize Finance
        ListReader = new StreamReader(Application.dataPath + "/Resources/Finance.txt");
        content = ListReader.ReadToEnd();
        words = content.Split(",");
        foreach (string word in words)
        {
            Finance.Add(new WordClass(word, topicValue));
        }

        //intialize Games
        ListReader = new StreamReader(Application.dataPath + "/Resources/Games.txt");
        content = ListReader.ReadToEnd();
        words = content.Split(",");
        foreach (string word in words)
        {
            Games.Add(new WordClass(word, topicValue));
        }

        //intialize Health
        ListReader = new StreamReader(Application.dataPath + "/Resources/Health.txt");
        content = ListReader.ReadToEnd();
        words = content.Split(",");
        foreach (string word in words)
        {
            Health.Add(new WordClass(word, topicValue));
        }

        //intialize Marketing
        ListReader = new StreamReader(Application.dataPath + "/Resources/Marketing.txt");
        content = ListReader.ReadToEnd();
        words = content.Split(",");
        foreach (string word in words)
        {
            Marketing.Add(new WordClass(word, topicValue));
        }

        //intialize Technology
        ListReader = new StreamReader(Application.dataPath + "/Resources/Technology.txt");
        content = ListReader.ReadToEnd();
        words = content.Split(",");
        foreach (string word in words)
        {
            Technology.Add(new WordClass(word, topicValue));
        }

        //intialize generic
        ListReader = new StreamReader(Application.dataPath + "/Resources/Generic.txt");
        content = ListReader.ReadToEnd();
        words = content.Split(",");
        foreach (string word in words)
        {
            Generic.Add(new WordClass(word, genericValue));
        }
    }

    //pick random topic
    public void PickTopic()
    {
        foreach(List<WordClass> list in Lists)
        {
            if(list != Generic)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].value = irrelevantValue;
                }
            }
        }
        switch (UnityEngine.Random.Range(0, 8))
        {
            case 0:
                for (int i = 0; i < Business.Count; i++)
                {
                    Business[i].value = topicValue;
                }
                CurrentTopic = Business;
                break;
            case 1:
                for (int i = 0; i < Education.Count; i++)
                {
                    Education[i].value = topicValue;
                }
                CurrentTopic = Education;
                break;
            case 2:
                for (int i = 0; i < EnvironmentList.Count; i++)
                {
                    EnvironmentList[i].value = topicValue;
                }
                CurrentTopic = EnvironmentList;
                break;
            case 3:
                for (int i = 0; i < Finance.Count; i++)
                {
                    Finance[i].value = topicValue;
                }
                CurrentTopic = Finance;
                break;
            case 4:
                for (int i = 0; i < Games.Count; i++)
                {
                    Games[i].value = topicValue;
                }
                CurrentTopic = Games;
                break;
            case 5:
                for (int i = 0; i < Health.Count; i++)
                {
                    Health[i].value = topicValue;
                }
                CurrentTopic = Health;
                break;
            case 6:
                for (int i = 0; i < Marketing.Count; i++)
                {
                    Marketing[i].value = topicValue;
                }
                CurrentTopic = Marketing;
                break;
            case 7:
                for (int i = 0; i < Technology.Count; i++)
                {
                    Technology[i].value = topicValue;
                }
                CurrentTopic = Technology;
                break;
        }
    }

    //get score
    public int GetScore(string word)
    {
        foreach(List<WordClass> list in Lists)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].word.Trim().ToUpper().Equals(word.Trim().ToUpper()))
                {
                    return list[i].value;
                }
            }
        }
        return 0;
    }

    //get score
    public void Discover(string word)
    {
        foreach (List<WordClass> list in Lists)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].word.Trim().ToUpper().Equals(word.Trim().ToUpper()))
                {
                    list[i].discovered = true;
                }
            }
        }
    }
}
