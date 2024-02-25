/*
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Subtegral.DialogueSystem.DataContainers;


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
        Debug.Log(levelData);


        button1 = option1.transform.parent.gameObject;
        button2 = option2.transform.parent.gameObject;

        startPos = button1.GetComponent<RectTransform>().position;

        if (levelData != null && levelData.DialogueNodeData.Count > 0)
        {
            DialogueNodeData block = levelData.DialogueNodeData[0];
            Debug.Log(block);

            DisplayBlock(block);
        }
        else
        {
            Debug.LogError("levelData is null or empty!");
        }
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
    public List<NodeLinkData> FindOutputConnections(DialogueNodeData nodeData, DialogueContainer container)
    {
        // Filter nodeLinks to find connections originating from the specified node
        return container.NodeLinks.Where(link => link.BaseNodeGUID == nodeData.NodeGUID).ToList();
    }
    public void DisplayBlock(DialogueNodeData block)
    {
        mainText.text = block.GetType().GetProperty("dialogueText").GetValue(block).ToString();

        outputs = FindOutputConnections(block, levelData).ToArray();

        switch (outputs.Length) {
            case 2:
                button1.GetComponent<RectTransform>().position = startPos;

                button2.SetActive(true);
                option2.text = outputs[1].PortName;
                goto case 1; // opasno bilo, pazi se od ova
            case 1: 
                button1.SetActive(true);

                option1.text = outputs[0].PortName;
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
        DialogueNodeData nextBlock = null;
        string targetGUID = outputs[index].TargetNodeGUID;

        foreach (var node in levelData.DialogueNodeData)
        {
            if (node.NodeGUID == targetGUID)
            {
                nextBlock = node;
            }
        }

        DisplayBlock(nextBlock);

        CheckBlock();
    }
}
*/
using System.Linq;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Subtegral.DialogueSystem.Editor;

public class DialogueSystem : MonoBehaviour {
    public Text mainText;
    public Text option1, option2;
    private GameObject button1, button2;
    public RectTransform center;
    private Vector3 startPos;
    private StoryBlock currentBlock;

    public MonoBehaviour gameplayObj;

    private DialogueContainer levelData;
    private NodeLinkData[] outputs;
    public string fileName = "level1";
    void Start()
    {
        levelData = Resources.Load<DialogueContainer>(fileName);

        button1 = option1.transform.parent.gameObject;
        button2 = option2.transform.parent.gameObject;

        startPos = button1.GetComponent<RectTransform>().position;

        if (levelData != null && levelData.DialogueNodeData.Count > 0)
        {
            DialogueNodeData block = levelData.DialogueNodeData[0];
            Debug.Log(block);

            DisplayBlock(block);
        }
        else
        {
            Debug.LogError("levelData is null or empty!");
        }
    }

    public void DisplayBlock(DialogueNodeData block)
    {
        mainText.text = block.DialogueText;

        outputs = levelData.NodeLinks.Where(link => link.BaseNodeGUID == block.NodeGUID).ToArray();

        switch (outputs.Length)
        {
            case 2:
                button1.GetComponent<RectTransform>().position = startPos;

                button2.SetActive(true);
                option2.text = outputs[1].PortName;
                goto case 1; // opasno bilo, pazi se od ova
            case 1:
                button1.SetActive(true);

                option1.text = outputs[0].PortName;
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

    public void ButtonClicked(int index)
    {
        DialogueNodeData nextBlock = null;
        string targetGUID = outputs[index].TargetNodeGUID;

        foreach (var node in levelData.DialogueNodeData)
        {
            if (node.NodeGUID == targetGUID)
            {
                nextBlock = node;
            }
        }

        DisplayBlock(nextBlock);

        CheckBlock();
    }

    void CheckBlock()
    {
        if (currentBlock.final != true)
            return;

        gameplayObj.GetComponent<GameplayObject>().StartGameplay();
        gameObject.SetActive(false);
    }
}