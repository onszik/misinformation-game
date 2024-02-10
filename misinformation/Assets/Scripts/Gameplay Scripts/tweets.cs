using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class tweets : MonoBehaviour, GameplayObject
{
    public tweetObj[] tweetObjs;

    public GameObject tweetPrefab;
    private tweetObj currentTweet;

    bool isDone = false;
    public GameObject button1, button2;
    public Transform spawnPoint;

    private int currentIndex = 0;

    private camera cameraScript;

    public Sidebar sidebar;
    private void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera>();
        sidebar = GameObject.Find("Sidebar").GetComponent<Sidebar>();
    }
    public void StartGameplay()
    {
        button1.SetActive(true);
        button2.SetActive(true);

        currentTweet = tweetObjs[currentIndex];

        Destroy(GameObject.FindGameObjectWithTag("tweet1"));

        tweetPrefab.SetActive(true);

        NewTweet();
    }
    public void NextButton()
    {
        button1.SetActive(true);
        button2.SetActive(true);

        currentIndex++;
        if (currentIndex >= tweetObjs.Length)
            currentIndex = 0;
        currentTweet = tweetObjs[currentIndex];

        Destroy(GameObject.FindGameObjectWithTag("tweet1"));
        NewTweet();
    }


    public void PostButton()
    {
        tweetObj twt = GameObject.FindGameObjectWithTag("tweet1").GetComponent<TweetInfo>().obj;

        scorescript.AddScore(twt.value * 2);
        scorescript1.AddScore(twt.value);

        cameraScript.Camerating1();
        
        GameObject sidebarTweet = GameObject.FindGameObjectWithTag("tweet1");

        sidebarTweet.GetComponent<TweetInfo>().UpdateInfo();

        PreviewTweet.Preview(sidebarTweet, spawnPoint.position, NextButton);

        button1.GetComponent<Button>().interactable = false;
        button2.GetComponent<Button>().interactable = false;
        Debug.Log("hi world");
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
