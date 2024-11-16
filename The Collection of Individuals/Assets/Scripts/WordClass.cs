using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordClass
{
    public string word;
    public int value;
    public bool discovered;

    public WordClass(string word, int value)
    {
        this.word = word;
        this.value = value;
        discovered = false;
    }
}
