using System;
using System.Linq;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour {
    public TMP_Text mainText;
    public TMP_Text option1, option2;
    [HideInInspector] public GameObject button1, button2;
    public RectTransform center;
    private Vector2 startPos;

    private DialogueContainer levelData;
    private NodeLinkData[] outputs;
    public string fileName = "level1";

    public GameObject container;

    private TweetSystem _tweetHandler;
    private DialogueNodeData currentBlock;

    public void AddTweetHandler(TweetSystem tweetHandler)
    {
        _tweetHandler = tweetHandler;
    }

    private DialogueContainer CopyDataForRuntime(DialogueContainer levelData)
    {
        DialogueContainer file = new DialogueContainer();
        file.NodeLinks = levelData.NodeLinks.ToList(); // Copy NodeLinks
        file.DialogueNodeData = levelData.DialogueNodeData.ToList(); // Copy DialogueNodeData
        file.ExposedProperties = levelData.ExposedProperties.ToList(); // Copy ExposedProperties
        file.CommentBlockData = levelData.CommentBlockData.ToList(); // Copy CommentBlockData

        return file;
    }
    void Start()
    {
        levelData = CopyDataForRuntime(Resources.Load<DialogueContainer>(fileName));

        button1 = option1.transform.parent.gameObject;
        button2 = option2.transform.parent.gameObject;

        startPos = button1.GetComponent<RectTransform>().position;

        if (levelData != null && levelData.DialogueNodeData.Count > 0)
        {
            DialogueNodeData block = levelData.DialogueNodeData[0];

            DisplayBlock(block);
        }
        else
        {
            Debug.LogError("levelData is null or empty!");
        }
    }

    public void DisplayBlock(DialogueNodeData block)
    {
        currentBlock = block;

        mainText.text = block.DialogueText;

        SetOutputsLinks(block);

        switch (block.DialogueText)
        {
            case "[Реакција на следачи]":
                var reactionTweets = FindConnectedTweets();

                _tweetHandler.DisplayReaction(reactionTweets.ToArray());
                container.SetActive(false);

                return;

            case "[True False]":
                var tfTweets = FindConnectedTweets();

                _tweetHandler.DisplayTrueFalse(tfTweets.ToArray());
                container.SetActive(false);

                return;
            case "[ScoreCheck]":
                switch (_tweetHandler.score)
                {
                    case 1:
                    case 2:
                        ButtonClicked(2);
                        break;
                    case 3:
                    case 4:
                        ButtonClicked(1);
                        break;
                    case >= 5:
                        ButtonClicked(0);
                        break;
                }
                return;
            case "[Kraj]":
                ScenesTransitions.NextScene();
                return;
        }

        switch (outputs.Length)
        {
            case 2:
                button1.GetComponent<RectTransform>().position = startPos;

                button1.SetActive(true);
                option1.text = outputs[0].PortName;

                button2.SetActive(true);
                option2.text = outputs[1].PortName;

                break;
            case 1:
                button1.SetActive(true);

                option1.text = outputs[0].PortName;
                button1.GetComponent<RectTransform>().position = center.position;

                button2.SetActive(false);

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
    }

    public void ButtonClicked(int index)
    {
        if (!container.activeInHierarchy)
            container.SetActive(true);

        DialogueNodeData nextBlock = GetNodeAtPort(index);

        CheckNextBlock(nextBlock);
    }
    public void ButtonClicked(string key = "Next")
    {
        if (!container.activeInHierarchy)
            container.SetActive(true);

        DialogueNodeData nextBlock = GetNodeByName(key);

        CheckNextBlock(nextBlock);
    }

    public void CheckNextBlock(DialogueNodeData nextBlock)
    {
        if (nextBlock.IsTweetNode)
        {
            currentBlock = nextBlock;
            SetOutputsLinks(nextBlock);

            container.SetActive(false);
            _tweetHandler.DisplayTweet(nextBlock);
        }
        else
        {
            DisplayBlock(nextBlock);
        }
    }

    public void SetOutputsLinks(DialogueNodeData node)
    {
        outputs = levelData.NodeLinks.Where(link => link.BaseNodeGUID == node.NodeGUID).ToArray();

        int i = 0;
        foreach (NodeLinkData n in outputs)
        {
            i++;
        }
    }

    public DialogueNodeData GetNodeAtPort(int index = 0)
    { 
        DialogueNodeData nextNode = GetNodeByGUID(outputs[index].TargetNodeGUID);

        if (nextNode == null)
        {
            Debug.LogError("No node linked, current node + " + mainText.text);
            nextNode = new DialogueNodeData();
        }

        return nextNode;
    }
    public DialogueNodeData GetNodeByName(string name)
    {
        DialogueNodeData nextNode = new DialogueNodeData();
        string targetGUID = "";

        foreach (var link in outputs)
        {
            if (link.PortName == name)
            {
                targetGUID = link.TargetNodeGUID;
                break;
            }
        }

        if (targetGUID == "")
        {
            Debug.LogError("Could not find node with name " + name);
        }
        else
        {
            nextNode = GetNodeByGUID(targetGUID);
        } 

        return nextNode;
    }
    public DialogueNodeData GetNodeByGUID(string targetGUID)
    {
        DialogueNodeData returnNode = new DialogueNodeData();

        foreach (var node in levelData.DialogueNodeData)
        {
            if (node.NodeGUID == targetGUID)
            {
                returnNode = node;
            }
        }

        if (returnNode == new DialogueNodeData())
        {
            Debug.LogError("Could not find node with GUID " + targetGUID);
        }

        return returnNode;
    }

    public List<DialogueNodeData> FindConnectedTweets()
    {
        List<DialogueNodeData> connected = new List<DialogueNodeData>();

        for (int i = 0; i < outputs.Length; i++)
        {
            var output = outputs[i];

            foreach (var node in levelData.DialogueNodeData)
            {
                if (!node.IsTweetNode)
                    continue;

                if (node.NodeGUID != output.TargetNodeGUID)
                    continue;

                connected.Add(node);
                break;
            }
        }

        return connected;
    }
}