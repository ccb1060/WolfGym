using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewsController : MonoBehaviour
{
    public TMP_Text bodyText;
    public TMP_Text headText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateText(string article)
    {
        string[] texts = article.Split(';');
        headText.text = texts[0];
        bodyText.text = texts[1];
    }
}
