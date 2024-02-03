using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryBlock {
    public string story;

    public string option1Text;
    public int option1Index; // ako e negativen broj ili 0 go vazit ko da nemat niso

    public string option2Text;
    public int option2Index;

    public int getChoiceNumber()
    {
        int numOfReplies = 0;

        if (option1Index > 0){
            numOfReplies++;
        }
        if (option2Index > 0)
        {
            numOfReplies++;
        }

        return numOfReplies;
    }

    public bool final = false;
}