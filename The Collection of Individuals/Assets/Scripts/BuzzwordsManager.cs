using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuzzwordsManager : MonoBehaviour
{
    //dictionaries to store words and connection values
    private Dictionary<string, int> Topical = new Dictionary<string, int>();
    private Dictionary<string, int> Generic = new Dictionary<string, int>();

    private void Start()
    {
        //intialize topical


        //intialize generic

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
        temp = Topical.Keys.ToArray()[(int)Random.Range(0, Topical.Keys.Count - 1)];
        Topical[temp] = 5;
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
