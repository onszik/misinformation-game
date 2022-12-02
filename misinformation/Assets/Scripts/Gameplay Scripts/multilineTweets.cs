using UnityEngine;

public class multilineTweets : MonoBehaviour, GameplayObject 
{
    public MultilineTweet[] tweetObjs;

    // public GameObject Timer;
    public GameObject mainTweet;

    private MultilineTweet currentTweet;

    private NewLine[] lines;
    private NewLine currentLine;

    public GameObject button2;

    private int currentIndex = 0;

    private camera cameraScript;
    private void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera>();
    }

    public void StartGameplay()
    {
        button2.SetActive(true);

        Destroy(GameObject.FindGameObjectWithTag("tweet1"));

        mainTweet.SetActive(true);

        NewTweet();
    }

    public void ClickedLine(int index)
    {
        currentLine = lines[index - 1];
    }

    public void PostButton()
    {
        if (currentLine == null)
            return;

        scorescript.AddScore(currentLine.value * 2);
        scorescript1.AddScore(currentLine.value);

        cameraScript.Camerating1();
        cameraScript.Sound();

        string text = currentLine.content;

        GameObject sidebarTweet = GameObject.FindGameObjectWithTag("tweet1").GetComponent<TweetInfo1>().UpdateInfo(text);
        sidebarTweet.transform.parent = transform;
        sidebarTweet.transform.localScale = new Vector3(2f, 2f, 1);
        sidebarTweet.transform.position = mainTweet.transform.position;

        PreviewTweet.Preview(sidebarTweet, mainTweet.transform.position, NewTweet);

        currentLine = null;
    }

    public void NewTweet()
    {
        currentIndex++;
        if (currentIndex >= tweetObjs.Length)
            currentIndex = 0;
        currentTweet = tweetObjs[currentIndex];

        mainTweet.GetComponent<TweetInfo1>().SetObject(currentTweet);
        lines = GameObject.FindGameObjectWithTag("tweet1").GetComponent<TweetInfo1>().obj.m_lines;
    }
}
