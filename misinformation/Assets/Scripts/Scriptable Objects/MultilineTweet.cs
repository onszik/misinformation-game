using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MultilineTweet", menuName = "Tweets/MultilineTweet", order = 1)]
//[System.Serializable]
public class MultilineTweet : ScriptableObject {
    [SerializeField] 
    public string username = "@NewUser";

    public int likes = 0;
    public int shares = 0;

    public NewLine[] m_lines = new NewLine[3];
}

