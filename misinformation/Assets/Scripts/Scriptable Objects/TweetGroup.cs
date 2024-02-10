using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Group", menuName = "Tweets/TweetGroup", order = 3)]
public class TweetGroup : ScriptableObject
{
    public tweetObj[] tweets;
}
