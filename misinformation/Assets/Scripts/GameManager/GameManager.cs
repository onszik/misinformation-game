using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public Text mainText;
    public Text option1, option2;
    public AudioSource buttonSound;
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
        currentBlock = block;
        mainText.text = block.story;

        int possibleChoices = block.getChoiceNumber();

        switch (possibleChoices) {
            case 0:
                button1.SetActive(false);
                button2.SetActive(false);
                break;
            case 1:
                button1.GetComponent<RectTransform>().position = center.position;
                button2.SetActive(false);
                break;
            case 2:
                button1.GetComponent<RectTransform>().position = startPos;
                button2.SetActive(true);
                option2.text = block.option2Text;
                break;
            default:
                print("Error - nepoznati odluki na blokot tekst " + block);
                break;
        }
    }
    void CheckBlock()
    {
        if (currentBlock.final != true)
            return;

        gameplayObj.GetComponent<tweets>().StartGameplay();

        Destroy(gameObject);
    }
    public void Button1Clicked()
    {
        DisplayBlock(blocks[currentBlock.option1Index - 1]);

        buttonSound.Play();
        CheckBlock();
    }
    public void Button2Clicked()
    {
        DisplayBlock(blocks[currentBlock.option2Index - 1]);

        buttonSound.Play();
        CheckBlock();
    }
}
