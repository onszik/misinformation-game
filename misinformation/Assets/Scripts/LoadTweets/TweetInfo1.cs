using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TweetInfo1 : MonoBehaviour
{
    public MultilineTweet obj;

    public GameObject prefab;
    public void SetObject(MultilineTweet tweet)
    {
        obj = tweet;

        DisplayInfo();
    }

    private void DisplayInfo()
    {
        transform.Find("Top/Username").GetComponent<TMP_Text>().text = obj.username;

        int i = 0;
        foreach (NewLine l in obj.m_lines)
        {
            transform.Find("Middle").GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = l.content;
            i++;
        }

        transform.Find("Bottom/Likes").GetComponent<TMP_Text>().text = "" + obj.likes;
        transform.Find("Bottom/Shares").GetComponent<TMP_Text>().text = "" + obj.shares;
    }

    public GameObject UpdateInfo(string text)
    {
        GameObject p = Instantiate(prefab) as GameObject;

        p.transform.Find("Top/Username").GetComponent<TMP_Text>().text = "@Me";
        p.transform.Find("Middle/Content").GetComponent<TMP_Text>().text = obj.username + " quoted: " + text;

        return p;
    }
}
