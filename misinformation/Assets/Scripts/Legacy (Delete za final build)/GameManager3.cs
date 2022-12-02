/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class StoryBlock1
{
    public string story;
    public string option1Text;
    public string option2Text;
    public StoryBlock1 option1Block;
    public StoryBlock1 option2Block;

    public StoryBlock1(string story, string option1Text = "", string option2Text = "", StoryBlock1 option1Block = null, StoryBlock1 option2Block = null)
    {
        this.story = story;
        this.option1Text = option1Text;
        this.option2Text = option2Text;
        this.option1Block = option1Block;
        this.option2Block = option2Block;
    }
}
public class GameManager3 : MonoBehaviour
{
    public Text mainText;
    public Button option1;
    public Button option2;
    private StoryBlock1 currentBlock;

    public multilineTweets gameplayObj;

    static StoryBlock1 block6 = new StoryBlock1("Don't worry about the details.");
    static StoryBlock1 block5 = new StoryBlock1("Lets begin with the journey...");
    static StoryBlock1 block4 = new StoryBlock1("You can only get so far with your own words. Thats why now we're going to make it look like people more famous than you said these things.", "Let's go", "Isn't that a bit immoral?", block5, block6);
    static StoryBlock1 block3 = new StoryBlock1("You've managed to get a small following, but now its time to get serious!", "I'm ready!", "How serious?", block4, block4);
    static StoryBlock1 block2 = new StoryBlock1("We don't have time for that!", "I'm goofy :", "Ok...", block3, block3);
    static StoryBlock1 block1 = new StoryBlock1("Congratulations on passing the first part! You managed to get a small following, but now we need to start getting serious...", "I'm serious.", "I don't think I'm ready yet!", block3, block2);

    private bool waitForClick = false;

    // Start is called before the first frame update
    void Start()
    {
        DisplayBlock(block1);
    }

    void DisplayBlock(StoryBlock1 block)
    {
        mainText.text = block.story;
        option1.GetComponentInChildren<Text>().text = block.option1Text;
        option2.GetComponentInChildren<Text>().text = block.option2Text;

        currentBlock = block;
    }
    void CheckBlock()
    {
        if (currentBlock == block5 || currentBlock == block6)
        {
            Destroy(option1.gameObject);
            Destroy(option2.gameObject);

            waitForClick = true;
        }
    }

    private void Update()
    {
        if (waitForClick && Input.GetMouseButton(0))
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