using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TweetInfo : MonoBehaviour
{
    public tweetObj obj;

    public void SetObject(tweetObj tweet)
    {
        obj = tweet;

        DisplayInfo();
    }

    private void DisplayInfo()
    {
        transform.Find("Top/Username").GetComponent<TMP_Text>().text = obj.username;
        transform.Find("Middle/Content").GetComponent<TMP_Text>().text = obj.content;
    }

    public void UpdateInfo()
    {
        transform.Find("Top/Username").GetComponent<TMP_Text>().text = obj.username;
        transform.Find("Middle/Content").GetComponent<TMP_Text>().text = obj.content;
        transform.Find("Bottom/Likes").GetComponent<TMP_Text>().text = "" + Random.Range(obj.value, obj.value * 2);
        transform.Find("Bottom/Shares").GetComponent<TMP_Text>().text = "" + Mathf.Clamp(Random.Range(obj.value / 2, obj.value), 0, obj.value);
    }
}
