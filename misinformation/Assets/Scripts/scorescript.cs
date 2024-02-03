using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scorescript : MonoBehaviour {

    public Text scoreText;
    public int scoreValue;

    public string text;

    public int goal = 100;
    public int lose = -100;

    private static scorescript instance;

    public AudioSource good, bad;

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

        if(scoreValue >= goal)
        {
            ScenesTransitions.NextScene();
        }


        if(scoreValue <= lose)
        {
            ScenesTransitions.LoadFail();
        }

        if (x > 0)
        {
            good.Play();
            LikesPopup.SpawnLikes();
        }
        else
        {
            bad.Play();
            LikesPopup.SpawnDislikes();
        }
    }

    void BackToNormal()
    {
        scoreText.text = text + " " + scoreValue;
    }
}

