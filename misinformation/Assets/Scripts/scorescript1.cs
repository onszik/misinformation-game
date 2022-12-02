using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scorescript1 : MonoBehaviour {

    public Text scoreText;
    public int scoreValue;

    public string text;

    private static scorescript1 instance;

    void Start()
    {
        scoreText = GetComponent<Text>();

        scoreText.text = text + " " + scoreValue;

        instance = this;
    }
    public static void AddScore(int x)
    {
        instance.scoreValue += x;

        instance.UpdateScore(x);
    }
    private void UpdateScore(int x)
    {
        if (scoreValue < 0)
        {
            scoreValue = 0;
        }

        if (x > 0)
        {
            scoreText.text = text + " " + scoreValue + "<color=lime> +" + x + "</color>";
        }
        else
        {
            scoreText.text = text + " " + scoreValue + "<color=red> " + x + "</color>";
        }

        Invoke("BackToNormal", 2f);
    }

    void BackToNormal()
    {
        scoreText.text = text + " " + scoreValue;
    }
}

