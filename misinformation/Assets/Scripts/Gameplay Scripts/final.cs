using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class final : MonoBehaviour, GameplayObject
{
    public GameObject tweetPrefab;
    private tweetObj currentTweet;

    bool isDone = false;
    public GameObject inputField;
    public GameObject postButton;
    public Transform spawnPoint;

    public Sidebar sidebar;

    public GameObject prefab;

    private void Start()
    {
        sidebar = GameObject.Find("Sidebar").GetComponent<Sidebar>();
    }

    public void StartGameplay()
    {
        postButton.SetActive(true);
        inputField.SetActive(true);

        Destroy(GameObject.FindGameObjectWithTag("tweet1"));
    }

    public void PostButton()
    {
        int likes = Random.Range(50, 200) * 2;
        int shares = Random.Range(50, 200);

        scorescript.AddScore(likes);
        scorescript1.AddScore(shares);

        GameObject p = Instantiate(prefab) as GameObject;

        p.transform.parent = transform;
        p.transform.position = spawnPoint.transform.position;

        p.transform.Find("Top/Username").GetComponent<TMP_Text>().text = "@Me";
        p.transform.Find("Middle/Content").GetComponent<TMP_Text>().text = ReadInput.text;
        p.transform.Find("Bottom/Likes").GetComponent<TMP_Text>().text = "" + likes;
        p.transform.Find("Bottom/Shares").GetComponent<TMP_Text>().text = "" + shares;
         
        PreviewTweet.Preview(p, spawnPoint.position, PlaceholderHAHA);

        postButton.GetComponent<Button>().interactable = false;
        inputField.SetActive(false);
    }

    public void PlaceholderHAHA()
    {
        postButton.GetComponent<Button>().interactable = true;
        inputField.SetActive(true);
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
        newtweet.transform.parent = transform;
        newtweet.GetComponent<TweetInfo>().SetObject(currentTweet);

        postButton.GetComponent<Button>().interactable = true;
    }
}
