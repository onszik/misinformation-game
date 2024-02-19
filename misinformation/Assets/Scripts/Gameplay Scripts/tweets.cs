using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class tweets : MonoBehaviour, GameplayObject
{
    private tweetObj[] tweetObjs;
    public TweetGroup[] tweetGroups;
    private int currentGroup = 0;

    public GameObject tweetPrefab;
    private tweetObj currentTweet;

    bool isDone = false;
    public GameObject button1, button2;
    public Transform spawnPoint;

    private int currentIndex = 0;

    private camera cameraScript;

    public Sidebar sidebar;

    private static tweets instance;
    public GameManager dialogue;

    private void Start()
    {
        instance = this;

        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera>();
        sidebar = GameObject.Find("Sidebar").GetComponent<Sidebar>();
    }

    public void StartGameplay()
    {
        button1.SetActive(true);
        button2.SetActive(true);

        instance.currentGroup++;
        tweetObjs = tweetGroups[currentGroup - 1].tweets;
        currentTweet = tweetObjs[currentIndex];

        Destroy(GameObject.FindGameObjectWithTag("tweet1"));

        tweetPrefab.SetActive(true);

        NewTweet();
    }

    public void Disable()
    {
        Destroy(GameObject.FindGameObjectWithTag("tweet1"));

        button1.SetActive(false);
        button2.SetActive(false);
    }

    public static void NextTweet()
    {
        instance.NextButton();
    }

    public void NextButton()
    {
        currentIndex++;
        if (currentIndex >= tweetObjs.Length)
            currentIndex = 0;
        currentTweet = tweetObjs[currentIndex];

        Destroy(GameObject.FindGameObjectWithTag("tweet1"));
        NewTweet();
    }

    public void PostButton()
    {
        button1.GetComponent<Button>().interactable = false;
        button2.GetComponent<Button>().interactable = false;

        GameObject sidebarTweet = GameObject.FindGameObjectWithTag("tweet1");
        sidebarTweet.GetComponent<TweetInfo>().UpdateInfo();

        PreviewTweet.Preview(sidebarTweet, spawnPoint.position, NextButton);

        tweetObj twt = sidebarTweet.GetComponent<TweetInfo>().obj;

        scorescript.AddScore(twt.value);

        cameraScript.Camerating1();

        if (currentTweet.uniqueResponse == true)
        {
            dialogue.gameObject.SetActive(true);
            dialogue.DisplayBlock(currentTweet.response);

            Disable();
        }
    }

    public void tweetty()
    {
        if (GameObject.FindGameObjectsWithTag("delete").Length <= 0 && !isDone)
        {
            NewTweet();

            isDone = true;
        }
    }

    public void NewTweet()
    {
        //currentIndex++;

        GameObject newtweet = Instantiate(tweetPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
        newtweet.transform.SetParent(transform, true);
        newtweet.GetComponent<TweetInfo>().SetObject(currentTweet);

        button1.GetComponent<Button>().interactable = true;
        button2.GetComponent<Button>().interactable = true;
    }
}
