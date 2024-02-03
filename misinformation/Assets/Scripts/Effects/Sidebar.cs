using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Sidebar : MonoBehaviour
{
    public Transform[] tweets;

    private List<Image> coloredObjs = new List<Image>();

    private void Start()
    {
        ScenesTransitions.BeforeSceneUnload += OnSceneUnloaded; // strav mi e da go ostavam vaka bez unsubscribe ama taka e na unity
        ScenesTransitions.OnSceneLoad += OnSceneLoaded; // strav mi e da go ostavam vaka bez unsubscribe ama taka e na unity

        tweets = new Transform[6];

        int i = 0;
        Transform[] sidebarTweets = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform t in sidebarTweets)
        {
            if (t.tag == "tweet2")
            {
                tweets[i] = t;
                i++;
            }
        }

        coloredObjs.Add(gameObject.GetComponent<Image>());
        coloredObjs.Add(transform.Find("Overlay").GetComponent<Image>());
        coloredObjs.Add(transform.Find("Overlay/Gradient").GetComponent<Image>());
    }
    public void AddTweet(GameObject tweet)
    {

        tweet.tag = "tweet2";

        Transform twt_transform = tweet.transform;

        twt_transform.parent = transform;
        twt_transform.SetSiblingIndex(0);
        twt_transform.localScale = new Vector3(1, 1, 1);
        twt_transform.localPosition = new Vector3(0, 440, -80);

        twt_transform.DOLocalMoveY(twt_transform.localPosition.y - 240, 1f);

        foreach (Transform t in tweets)
        {
            t.DOLocalMoveY(t.localPosition.y - 193, 1f);
        }

        if (tweets[5] != null)
            Destroy(tweets[5].gameObject);

        for (int i = 5; i >= 1; i--)
        {
            tweets[i] = tweets[i - 1];
        }

        tweets[0] = twt_transform;
    }

    void OnSceneUnloaded()
    {
        transform.parent = null;
    }

    void OnSceneLoaded()
    {
        DontDestroyOnLoad(gameObject);

        GameObject oldSidebar = GameObject.FindGameObjectWithTag("Sidebar");
        transform.parent = oldSidebar.transform.parent;

        Color newColor = oldSidebar.GetComponent<Image>().color;

        foreach (Image i in coloredObjs)
        {
            i.color = newColor;
        }

        Destroy(oldSidebar);
    }
}
