/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager5 : MonoBehaviour {
    public Text mainText;
    public Text option1, option2;
    private GameObject button1, button2;
    public RectTransform center;
    private Vector3 startPos;
    private StoryBlock currentBlock;

    public Beef gameplayObj;

    static StoryBlock block8 = new StoryBlock("Good luck! And remember, don't make any friends!");
    static StoryBlock block7 = new StoryBlock("You are getting more and more popular everyday keep working and you will reach the top!", "Lets go then!", block8);
    static StoryBlock block5 = new StoryBlock("You will target famous people and argue with them so that you can get even more popular, yo understand me?", "Yes", block7);
    static StoryBlock block6 = new StoryBlock("Don't worry!", "Ok, ok, just tell me what to do", block5);
    static StoryBlock block4 = new StoryBlock("Todays mission is to try and argue with famous influencers, yep you heard me right!", "Sure", "Isn't that a bit too much?", block5, block6);
    //static StoryBlock block3 = new StoryBlock("You've managed to get a small following, but now its time to get serious!", "I'm ready!", "How serious?", block4, block4);
    static StoryBlock block2 = new StoryBlock("We don't have time for that!", "Ok...", block4);
    static StoryBlock block1 = new StoryBlock("Congratulations on passing again! You managed to getting a good amount off following, i hope you will contiue like this", "Thank you", "Um... what?", block4, block2);

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