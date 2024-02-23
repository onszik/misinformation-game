using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class DialogueSystem : MonoBehaviour {
    public Text mainText;
    public Text option1, option2;
    private GameObject button1, button2;
    public RectTransform center;
    private Vector3 startPos;
    private StoryBlock currentBlock;

    public MonoBehaviour gameplayObj;

    public StoryBlock[] blocks;

    public string fileName = "level1";
    private DialogueContainer levelData;

    //private List<StoryBlock> storyBlocks = new List<StoryBlock>();
    private NodeLinkData[] outputs;

    void Start()
    {
        LoadGraphFromFile();

        button1 = option1.transform.parent.gameObject;
        button2 = option2.transform.parent.gameObject;

        startPos = button1.GetComponent<RectTransform>().position;

        DisplayBlock(levelData.nodeData[0]);
    }
    public void LoadGraphFromFile()
    {
        levelData = Resources.Load<DialogueContainer>(fileName);
        if (levelData == null)
        {
            Debug.LogError("No file found with the specified name \"{fileName}\"");
            return;
        }
    }
    public List<NodeLinkData> FindOutputConnections(NodeData nodeData, DialogueContainer container)
    {
        // Filter nodeLinks to find connections originating from the specified node
        return container.nodeLinks.Where(link => link.baseNodeGUID == nodeData.GUID).ToList();
    }
    public void DisplayBlock(NodeData block)
    {
        if (block.nodeType != "Dialogue")
            return;

        

        mainText.text = block.GetType().GetProperty("dialogueText").GetValue(block).ToString();

        outputs = FindOutputConnections(block, levelData).ToArray();

        switch (outputs.Length) {
            case 2:
                button1.GetComponent<RectTransform>().position = startPos;

                button2.SetActive(true);
                option2.text = outputs[1].portName;
                goto case 1; // opasno bilo, pazi se od ova
            case 1: 
                button1.SetActive(true);

                option1.text = outputs[0].portName;
                break;
            case 0:
                button1.SetActive(false);
                button2.SetActive(false);
                Debug.LogWarning("Nemat outputs block " + block);
                break;

            // v ako ne ojt ko sho trebit... 
            case > 2:
                Debug.LogWarning("Sistemov ne e napraen za da podrzi pojke od 2 outputs vomomento, posledovatelni ports se ignorirani :)");
                break;
            default:
                Debug.LogWarning("Neznam sho se desi, ama nesho se zaeba so output konekciite na node " + block);
                break;
        }

        currentBlock = new StoryBlock();
    }
    void CheckBlock()
    {

        if (currentBlock.final != true)
            return;

        gameplayObj.GetComponent<GameplayObject>().StartGameplay();
        gameObject.SetActive(false);
    }
    public void ButtonClicked(int index)
    {
        NodeData nextBlock = null;
        string targetGUID = outputs[index].targetNodeGUID;

        foreach (var node in levelData.nodeData)
        {
            if (node.GUID == targetGUID)
            {
                nextBlock = node;
            }
        }

        DisplayBlock(nextBlock);

        CheckBlock();
    }
}