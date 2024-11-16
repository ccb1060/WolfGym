using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewsController : MonoBehaviour
{
    public TMP_Text bodyText;
    string change = "CHANGE";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            UpdateText();
    }

    void UpdateText()
    {
        bodyText.text = change;
    }
}
