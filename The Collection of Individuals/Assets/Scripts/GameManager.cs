using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] NewsManager postManager;
    int followCount;

    [SerializeField] GameObject progressBar;
    float maxtime = 0;
    //The player's current score
    [SerializeField] int playerScore = 0;

    //How long the player has until the article changes
    [SerializeField] float timeUntilChange = 0;
    // Start is called before the first frame update
    void Start()
    {
        maxtime = 5;
        timeUntilChange = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilChange < 0)
        {
            postManager.changeArticle();
            maxtime = 5 - playerScore / 10;
            timeUntilChange = maxtime;
        }

        progressBar.GetComponent<UnityEngine.UI.Image>().fillAmount = timeUntilChange / maxtime;
        progressBar.GetComponent<UnityEngine.UI.Image>().color = new Color((1 - timeUntilChange / maxtime), 255, 0);
        timeUntilChange -= Time.deltaTime;
    }
}
