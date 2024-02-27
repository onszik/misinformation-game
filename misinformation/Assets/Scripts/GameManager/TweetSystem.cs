using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Subtegral.DialogueSystem.DataContainers;


public class TweetSystem : MonoBehaviour
{
    public GameObject tweetPrefab;

    public Transform spawnPoint;

    private camera cameraScript;

    //public Sidebar sidebar;

    private static TweetSystem instance;
    public DialogueSystem _dialogue;

    public GameObject container;

    public GameObject reactionTweetContainer;

    public GameObject trueFalseContainer;
    [HideInInspector] public int score = 0;
    private List<DialogueNodeData> trueFalseTweets;

    private void Start()
    {
        instance = this;

        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera>();
        //sidebar = GameObject.Find("Sidebar").GetComponent<Sidebar>();
        _dialogue.AddTweetHandler(instance);

        container.SetActive(false);
    }

    public void DisplayTweet(DialogueNodeData block)
    {
        container.SetActive(true);
        container.transform.Find("Next").GetComponent<Button>().interactable = true;
        container.transform.Find("Post").GetComponent<Button>().interactable = true;

        SpawnTweet(block, container.transform);
    }

    public GameObject SpawnTweet(DialogueNodeData block, Transform parent)
    {
        GameObject newtweet = Instantiate(tweetPrefab, spawnPoint.position, Quaternion.identity, container.transform);
        newtweet.transform.SetParent(parent, true);
        newtweet.transform.localScale = new Vector2(2.5f, 2.5f);
        newtweet.GetComponent<TweetInfo>().SetObject(block);

        return newtweet;
    }

    public void NextButton()
    {
        Destroy(GameObject.FindGameObjectWithTag("tweet1"));

        DialogueNodeData newBlock = _dialogue.GetNodeAtPort(0);

        DisplayTweet(newBlock);
        _dialogue.SetOutputsLinks(newBlock);
    }

    public void PostButton()
    {
        container.transform.Find("Next").GetComponent<Button>().interactable = false;
        container.transform.Find("Post").GetComponent<Button>().interactable = false;

        GameObject sidebarTweet = GameObject.FindGameObjectWithTag("tweet1");
        sidebarTweet.GetComponent<TweetInfo>().UpdateInfo();

        DialogueNodeData twt = sidebarTweet.GetComponent<TweetInfo>().obj;

        scorescript.AddScore(twt.Value);
        chaosMeter.AddScore(twt.Value);

        cameraScript.Camerating1();

        PreviewTweet.Preview(sidebarTweet, spawnPoint.position, OnFinish);
    }

    void OnFinish()
    {
        _dialogue.ButtonClicked("Post");

        container.SetActive(false);
    }

    public void DisplayReaction(DialogueNodeData[] reactionTweets)
    {
        reactionTweetContainer.SetActive(true);

        TweetInfo[] tweetObjs = reactionTweetContainer.GetComponentsInChildren<TweetInfo>();

        int i = 0;

        foreach (DialogueNodeData node in reactionTweets)
        {
            if (i >= tweetObjs.Length)
                break;

            tweetObjs[i].SetObject(node);
            i++;
        }
    }

    public void HideReactions()
    {
        reactionTweetContainer.SetActive(false);
        _dialogue.container.SetActive(true);

        _dialogue.ButtonClicked("Next");
    }

    public void DisplayTrueFalse(DialogueNodeData[] tweets)
    {
        trueFalseContainer.SetActive(true);

        trueFalseTweets = new List<DialogueNodeData>(tweets);

        SpawnTweet(trueFalseTweets[0], trueFalseContainer.transform);
    }

    public void UpdateTrueFalse()
    {
        if (trueFalseTweets.Count <= 1)
        {
            HideTrueFalse();
            return;
        }

        trueFalseTweets.Remove(trueFalseTweets[0]);
        SpawnTweet(trueFalseTweets[0], trueFalseContainer.transform);
        print("Remaining tweets: " + trueFalseTweets.Count);
    }

    public void TrueFalseButton(bool truefalse)
    {
        cameraScript.Sound();

        var current = trueFalseTweets[0];
        print("Input: " + truefalse + "; Target :" + current.Value);
        if ((truefalse == true && current.Value == 1) || (truefalse == false && current.Value == 0))
        {
            score++;
        }
        print("Score: " + score);

        Destroy(GameObject.FindGameObjectWithTag("tweet1"));
        UpdateTrueFalse();
    }
    public void HideTrueFalse()
    {
        trueFalseContainer.SetActive(false);
        _dialogue.container.SetActive(true);

        _dialogue.ButtonClicked("Next");
    }
}
