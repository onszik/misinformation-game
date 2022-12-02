using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TweetInfo3 : MonoBehaviour {
    public HaterTweet obj;

    public void SetObject(HaterTweet tweet)
    {
        obj = tweet;

        DisplayInfo();
    }

    private void DisplayInfo()
    {
        transform.Find("Top/Username").GetComponent<TMP_Text>().text = obj.username;
        transform.Find("Middle/Content").GetComponent<TMP_Text>().text = obj.content;
    }

    public void UpdateInfo(string text, int likes = 10, int shares = 0)
    {
        transform.Find("Top/Username").GetComponent<TMP_Text>().text = obj.username;
        transform.Find("Middle/Content").GetComponent<TMP_Text>().text = text;
        transform.Find("Bottom/Likes").GetComponent<TMP_Text>().text = "" + likes;
        transform.Find("Bottom/Shares").GetComponent<TMP_Text>().text = "" + Mathf.Clamp(shares, 0, 9999);
    }
}
