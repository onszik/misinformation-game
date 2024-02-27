using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Subtegral.DialogueSystem.DataContainers;
using TMPro;

public class TweetInfo : MonoBehaviour
{
    public DialogueNodeData obj;

    public void SetObject(DialogueNodeData tweet)
    {
        obj = tweet;

        DisplayInfo();
    }

    private void DisplayInfo()
    {
        transform.Find("Top/Username").GetComponent<TMP_Text>().text = obj.Username;
        transform.Find("Middle/Content").GetComponent<TMP_Text>().text = obj.DialogueText;
    }

    public void UpdateInfo()
    {
        transform.Find("Top/Username").GetComponent<TMP_Text>().text = obj.Username;
        transform.Find("Middle/Content").GetComponent<TMP_Text>().text = obj.DialogueText;
        transform.Find("Bottom/Likes").GetComponent<TMP_Text>().text = "" + Random.Range(obj.Value, obj.Value * 2);
        transform.Find("Bottom/Shares").GetComponent<TMP_Text>().text = "" + Mathf.Clamp(Random.Range(obj.Value / 2, obj.Value), 0, obj.Value);
    }
}
