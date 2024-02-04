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

    public bool final = false;

    /*
    public StoryBlock(string story, string option1Text = "", string option2Text = "", StoryBlock option1Block = null, StoryBlock option2Block = null)
    {
        this.story = story;
        this.option1Text = option1Text;
        this.option2Text = option2Text;
        this.option1Block = option1Block;
        this.option2Block = option2Block;
    }

    // maki ovie dve se overload methods za za imat opcija za eden button i bez button
    public StoryBlock(string story, string option1Text = "", StoryBlock option1Block = null)
    {
        this.story = story;
        this.option1Text = option1Text;
        this.option1Block = option1Block;
    }

    public StoryBlock(string story)
    {
        this.story = story;
    }
    */
}