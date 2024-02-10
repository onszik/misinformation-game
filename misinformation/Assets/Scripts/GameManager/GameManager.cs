using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public Text mainText;
    public Text option1, option2;
    private GameObject button1, button2;
    public RectTransform center;
    private Vector3 startPos;
    private StoryBlock currentBlock;

    public MonoBehaviour gameplayObj;

    public StoryBlock[] blocks;

    void Start()
    {
        button1 = option1.transform.parent.gameObject;
        button2 = option2.transform.parent.gameObject;

        startPos = button1.GetComponent<RectTransform>().position;

        DisplayBlock(blocks[0]);
    }

    void DisplayBlock(StoryBlock block)
    {
        mainText.text = block.story;

        if (block.option1Index > 0) // provervit dali imat samo story, eden button ili de buttons i gi enablevit ko so trebit
        {
            button1.SetActive(true);

            option1.text = block.option1Text;


            if (block.option2Index > 0)
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
        if (currentBlock.final != true)
            return;
        
        
        Destroy(gameObject);
        gameplayObj.GetComponent<GameplayObject>().StartGameplay();
    }
    public void Button1Clicked()
    {
        DisplayBlock(blocks[currentBlock.option1Index - 1]);

        CheckBlock();
    }
    public void Button2Clicked()
    {
        DisplayBlock(blocks[currentBlock.option2Index - 1]);

        CheckBlock();
    }
    
}
