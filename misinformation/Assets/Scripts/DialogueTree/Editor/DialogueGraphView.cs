using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;
using System.Linq;
using Button = UnityEngine.UIElements.Button;

public class DialogueGraphView : GraphView
{
    public readonly Vector2 defaultNodeSize = new Vector2(200, 500);

    public DialogueGraphView()
    {
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        AddElement(GenerateEntryPointNode());
    }

    private Port GeneratePort(Node node, Direction portDir, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDir, capacity, typeof(float)); // random type
    }

    private DialogueNode GenerateEntryPointNode()
    {
        DialogueNode node = new DialogueNode
        {
            title = "StartPoint",
            GUID = GUID.Generate().ToString(),
            dialogueText = "Start",
            entryPoint = true
        };

        Port generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(x: 100, y: 200, width: 100, height: 150));
        return node;
    }

    public void CreateNode(string type, string name)
    {
        //AddElement(CreateDialogueNode(name));
        AddElement(CreateNodeOfType(type, name));
    }
    public void CreateTweet(string name)
    {
        AddElement(CreateTweetNode(name));
    }

    public Node CreateNodeOfType(string type, string name)
    {
        Node newNode = new Node();

        switch (type) {
            case "Dialogue":
                newNode = CreateDialogueNode(name);
                break;
            case "Tweet":
                newNode = CreateTweetNode(name);
                break;
            /*
            case "Exec":
            case "Executive":
                newNode = CreateExecNode(name);
                break;
            */
            default:
                Debug.LogError("Invalid node type, defaulting to Dialogue Node");
                newNode = CreateDialogueNode(name);
                break;
        }

        newNode.Add(new ResizableElement());

        var inputPort = GeneratePort(newNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        newNode.inputContainer.Add(inputPort);

        newNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        newNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
        newNode.RefreshExpandedState();
        newNode.RefreshPorts();

        return newNode;
    }

    public DialogueNode CreateDialogueNode(string name)
    {
        DialogueNode dialogueNode = new DialogueNode
        {
            title = name,
            dialogueText = name,
            GUID = GUID.Generate().ToString(),
        };

        Button button = new Button(clickEvent: () => { 
            AddChoicePort(dialogueNode); 
        });
        dialogueNode.titleContainer.Add(button);
        button.text = "+";

        var textField = new TextField(string.Empty) {
            name = "Content Field",
            value = "Dialogue",
            multiline = true,
            style = {
                flexWrap = Wrap.Wrap,
                flexDirection = FlexDirection.Row,
                maxWidth = 250,
                minWidth = 50,
                whiteSpace = WhiteSpace.Normal
            }
        };

        textField.RegisterValueChangedCallback(evt =>
        {
            dialogueNode.dialogueText = evt.newValue;
        });
        dialogueNode.mainContainer.Add(textField);

        return dialogueNode;
    }
    public TweetNode CreateTweetNode(string name)
    {
        TweetNode tweetNode = new TweetNode
        {
            title = name,
            GUID = GUID.Generate().ToString(),
        };

        var nameField = new TextField(string.Empty)
        {
            name = "Username",
            value = "@NewUser",
            multiline = false,
            style = {
                flexWrap = Wrap.Wrap,
                flexDirection = FlexDirection.Row,
                maxWidth = 250,
                minWidth = 50,
                whiteSpace = WhiteSpace.Normal
            }
        };

        nameField.RegisterValueChangedCallback(evt =>
        {
            tweetNode.username = evt.newValue;
        });
        tweetNode.mainContainer.Add(nameField);

        var textField = new TextField(string.Empty)
        {
            name = "Content Field",
            value = "Tweet Body",
            multiline = true,
            style = {
                flexWrap = Wrap.Wrap,
                flexDirection = FlexDirection.Row,
                maxWidth = 250,
                minWidth = 50,
                whiteSpace = WhiteSpace.Normal
            }
        };

        textField.RegisterValueChangedCallback(evt =>
        {
            tweetNode.dialogueText = evt.newValue;
        });
        tweetNode.mainContainer.Add(textField);

        var valueField = new IntegerField(string.Empty)
        {
            name = "Content Field",
            value = 0,
            style = {
                flexWrap = Wrap.Wrap,
                flexDirection = FlexDirection.Row,
                maxWidth = 250,
                minWidth = 50,
                whiteSpace = WhiteSpace.Normal
            }
        };

        valueField.RegisterValueChangedCallback(evt =>
        {
            tweetNode.value = evt.newValue;
        });
        tweetNode.mainContainer.Add(valueField);

        AddBasicPort(tweetNode, "Next");
        AddBasicPort(tweetNode, "Post");

        return tweetNode;
    }
    public void AddBasicPort(Node dialogueNode, string portName = "")
    {
        var generatedPort = GeneratePort(dialogueNode, Direction.Output);

        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        oldLabel.text = portName;

        generatedPort.portName = portName;

        dialogueNode.outputContainer.Add(generatedPort);
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
    }

    public void AddChoicePort(Node dialogueNode, string overriddenPortName = "")
    {
        var generatedPort = GeneratePort(dialogueNode, Direction.Output);

        var outputPortCount = dialogueNode.outputContainer.Query(name: "connector").ToList().Count;
        string outputPortName = $"Choice {outputPortCount}";

        var choicePortName = string.IsNullOrEmpty(overriddenPortName)
            ? $"Choice {outputPortCount + 1}"
            : overriddenPortName;

        var deleteButton = new Button(() => RemovePort(dialogueNode, generatedPort))
        {
            text = "x"
        };
        generatedPort.contentContainer.Add(deleteButton);

        var textField = new TextField {
            name = "Choice Field",
            value = choicePortName,
            multiline = true,
            style = {
                maxWidth = 200,
                minWidth = 50,
                whiteSpace = WhiteSpace.Normal,
                overflow = Overflow.Visible,
                flexWrap = Wrap.Wrap,
                flexGrow = 1
            }
        };
        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        generatedPort.contentContainer.Add(textField);

        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(oldLabel);

        generatedPort.portName = choicePortName;

        dialogueNode.outputContainer.Add(generatedPort);
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compariblePorts = new List<Port>();

        foreach (Port p in ports)
        {
            if (startPort != p && startPort.node != p.node)
            {
                compariblePorts.Add(p);
            }
        }

        return compariblePorts;
    }

    private void RemovePort(Node dialogueNode, Port generatedPort)
    {
        var connectedEdges = edges.ToList().Where(edge =>
            edge.output == generatedPort || edge.input == generatedPort);

        foreach (var edge in connectedEdges)
        {
            edge.input.Disconnect(edge);
            edge.output.Disconnect(edge);
            RemoveElement(edge);
        }

        dialogueNode.outputContainer.Remove(generatedPort);

        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
    }
}
