using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tweet", menuName = "Tweets/Tweet", order = 1)]
public class tweetObj : ScriptableObject
{
    public string username = "@NewUser";
    public string content = "A wonderful serenity has taken possession of my entire soul, like these sweet mornings of spring which I enjoy with my whole heart. I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine.";

    public int likes = 0;
    public int shares = 0;

    public int value = 0; //<== ova kazvit dali e dobar ili los isto ko vo drugono;
    public int chaosLevel = 0;
}
