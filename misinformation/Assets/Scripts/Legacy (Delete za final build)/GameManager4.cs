/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class GameManager4 : MonoBehaviour {
    public Text mainText;
    public Text option1, option2;
    private GameObject button1, button2;
    public RectTransform center;
    private Vector3 startPos;
    private StoryBlock currentBlock;

    public multilineTweets gameplayObj;

    static StoryBlock block8 = new StoryBlock("Good luck! And remember, the wilder the better.");
    static StoryBlock block7 = new StoryBlock("We can take a little bit out of context, and nobody will notice. Making someone famous sound crazy is a great way to get some clout for yourself", "Lets go then!", block8);
    static StoryBlock block5 = new StoryBlock("We're gonna quote some famous people on our page. But we're not exactly going to be doing the most accurate job, if you understand what i'm saying?", "Go on", block7);
    static StoryBlock block6 = new StoryBlock("Don't worry about the details!", "Ok, ok, just tell me what to do", block5);
    static StoryBlock block4 = new StoryBlock("You can only get so far with your own words. Thats why now we're going to make it look like people more famous than you said these things.", "Let's go", "Isn't that a bit immoral?", block5, block6);
    //static StoryBlock block3 = new StoryBlock("You've managed to get a small following, but now its time to get serious!", "I'm ready!", "How serious?", block4, block4);
    static StoryBlock block2 = new StoryBlock("We don't have time for that!", "Ok...", block4);
    static StoryBlock block1 = new StoryBlock("Congratulations on passing the first part! You managed to get a small following, but now we need to start getting serious...", "I'm serious.", "I don't think I'm ready yet!", block4, block2);

    void Start()
    {
        button1 = option1.transform.parent.gameObject;
        button2 = option2.transform.parent.gameObject;

        startPos = button1.GetComponent<RectTransform>().position;

        DisplayBlock(block1);
    }

    void DisplayBlock(StoryBlock block)
    {
        mainText.text = block.story;

        if (block.option1Block != null) // provervit dali imat samo story, eden button ili de buttons i gi enablevit ko so trebit
        {
            button1.SetActive(true);

            option1.text = block.option1Text;

            if (block.option2Block != null)
            {
                button1.GetComponent<RectTransform>().position = startPos;

                button2.SetActive(true);
                option2.text = block.option2Text;
            }
            else
            {

                button1.GetComponent<RectTransform>().position = center.position;

                button2.SetActive(false);
            }
        }
        else
        {
            button1.SetActive(false);
            button2.SetActive(false);
        }

        currentBlock = block;
    }
    void CheckBlock()
    {
        if (currentBlock == block8)
        {
            Destroy(gameObject);

            gameplayObj.StartGameplay();
        }
    }
    public void Button1Clicked()
    {
        DisplayBlock(currentBlock.option1Block);

        CheckBlock();
    }
    public void Button2Clicked()
    {
        DisplayBlock(currentBlock.option2Block);

        CheckBlock();
    }

}
*/