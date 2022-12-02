using UnityEngine;
using UnityEngine.UI;

public class Beef : MonoBehaviour, GameplayObject
{
    public HaterTweet[] tweetObjs;

    public GameObject tweetPrefab;
    private HaterTweet currentTweet;

    public bool isDone = false;

    public GameObject[] buttons;
    private Text[] textObjs;

    public Transform spawnPoint;

    private int currentIndex = 0;

    public Sidebar sidebar;
    private void Start()
    {
        sidebar = GameObject.Find("Sidebar").GetComponent<Sidebar>();

        textObjs = new Text[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            textObjs[i] = buttons[i].GetComponentInChildren<Text>();
        }
    }
    public void StartGameplay()
    {
        foreach (GameObject b in buttons)
        {
            b.SetActive(true);
        }

        Destroy(GameObject.FindGameObjectWithTag("tweet1"));

        NewTweet();
    }

    public void Button(int index)
    {
        HaterTweet twt = currentTweet;

        int val = twt.replies[index - 1].value;

        int likes = val * 2 + Random.Range(0, val);
        int shares = val + Random.Range(0, val);

        scorescript.AddScore(likes);
        scorescript1.AddScore(shares);

        GameObject tweet = GameObject.FindGameObjectWithTag("tweet1");
        tweet.GetComponent<TweetInfo3>().UpdateInfo("Replying to " + twt.name + ": " + twt.replies[index - 1].content, likes, shares);

        PreviewTweet.Preview(tweet, spawnPoint.position, NewTweet);

        foreach (GameObject b in buttons)
        {
            b.GetComponent<Button>().interactable = false;
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
        currentIndex++;
        if (currentIndex >= tweetObjs.Length) { currentIndex = 0; }
        currentTweet = tweetObjs[currentIndex];

        GameObject newtweet = Instantiate(tweetPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
        newtweet.transform.SetParent(transform, true);
        newtweet.GetComponent<TweetInfo3>().SetObject(currentTweet);

        for (int i = 0; i < buttons.Length; i++)
        {
            textObjs[i].text = currentTweet.replies[i].content;
            buttons[i].GetComponent<Button>().interactable = true;
        }

        foreach (GameObject b in buttons)
        {
            b.GetComponent<Button>().interactable = true;
        }
    }
}
