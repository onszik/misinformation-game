/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour {
    public Text mainText;
    public Text option1, option2;
    private GameObject button1, button2;
    public RectTransform center;
    private Vector3 startPos;
    private StoryBlock currentBlock;

    public tweets gameplayObj;

    static StoryBlock block5 = new StoryBlock("Lets begin with the journey...");
    static StoryBlock block4 = new StoryBlock("The more controversial, the better! Don't worry so much about posting the truth if you know you an get a reaction", "Awesome, I'm ready!", block5);
    static StoryBlock block3 = new StoryBlock("Right now, you're nobody, so lets try to get to 100 followers for a start.", "Lets go!", "Any tips?", block5, block4);
    static StoryBlock block2 = new StoryBlock("Today, we're gonna do a little trolling! The objective for you will be to start spreading as much misinformation as you can", "Heck yeah!", "Uh, are you sure...", block3, block3);
    static StoryBlock block1 = new StoryBlock("Congratulats on starting this adventure! We're glad to have you", "Thanks :D", block2);

    void Start()
    {
        button1 = option1.transform.parent.gameObject;
        button2 = option2.transform.parent.gameObject;

        startPos = button1.GetComponent<RectTransform>().position;

        //DisplayBlock(block1);
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
        if (currentBlock == null)
        {
            Destroy(gameObject);

            gameplayObj.StartGameplay();
        }
    }
    public void Button1Clicked()
    {

        //DisplayBlock(currentBlock.option1Block);

        CheckBlock();
    }
    public void Button2Clicked()
    {
        //DisplayBlock(currentBlock.option2Block);

        CheckBlock();
    }

}
*/