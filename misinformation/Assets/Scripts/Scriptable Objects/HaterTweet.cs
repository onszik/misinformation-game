using UnityEngine;

[CreateAssetMenu(fileName = "Tweet", menuName = "Tweets/HaterTweet", order = 3)]
public class HaterTweet : ScriptableObject {
    public string username = "@NewUser";
    public string content = "A wonderful serenity has taken possession of my entire soul, like these sweet mornings of spring which I enjoy with my whole heart. I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine.";

    public int likes = 0;
    public int shares = 0;

    public NewLine[] replies;
}