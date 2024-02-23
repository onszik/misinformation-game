using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Tweet", menuName = "Tweets/Tweet", order = 1)]
public class tweetObj : ScriptableObject, DialogueElement
{
    public string username = "@NewUser";
    public string content = "A wonderful serenity has taken possession of my entire soul, like these sweet mornings of spring which I enjoy with my whole heart. I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine.";

    public int value = 0; //<== ova kazvit dali e dobar ili los isto ko vo drugono;
    public int chaos = 0;

    public bool uniqueResponse = false;
    //[ShowIf("uniqueResponse", true)] <- najdi nacin da se naprajt ova
    public StoryBlock response;
}

#if UNITY_EDITOR
[CustomEditor(typeof(tweetObj))]
class MyClassEditor : Editor {
    public override void OnInspectorGUI()
    {
        tweetObj self = (tweetObj)target;
        serializedObject.Update();
        if (self.uniqueResponse == true)
        {
            DrawDefaultInspector();
        }
        else
        {
            DrawPropertiesExcluding(serializedObject, "response");
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif