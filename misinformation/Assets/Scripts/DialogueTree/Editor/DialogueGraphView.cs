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

    private Port GeneratePort(DialogueNode node, Direction portDir, Port.Capacity capacity = Port.Capacity.Single)
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

    public void CreateNode(string name)
    {
        AddElement(CreateDialogueNode(name));
    }

    public DialogueNode CreateDialogueNode(string name)
    {
        DialogueNode dialogueNode = new DialogueNode
        {
            title = name,
            dialogueText = name,
            GUID = GUID.Generate().ToString(),
        };

        dialogueNode.Add(new ResizableElement());

        var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);

        dialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        Button button = new Button(clickEvent: () => { 
            AddChoicePort(dialogueNode); 
        });
        dialogueNode.titleContainer.Add(button);
        button.text = "+";

        var textField = new TextField(string.Empty) {
            style = {
                flexWrap = Wrap.Wrap,
                flexDirection = FlexDirection.Row,
                maxWidth = 250,
                minWidth = 50,
                whiteSpace = WhiteSpace.Normal
            }
        };

        textField.multiline = true;
        textField.RegisterValueChangedCallback(evt =>
        {
            dialogueNode.dialogueText = evt.newValue;
        });
        textField.SetValueWithoutNotify(dialogueNode.title);
        dialogueNode.mainContainer.Add(textField);
        

        dialogueNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();

        return dialogueNode;
    }

    public void AddChoicePort(DialogueNode dialogueNode, string overriddenPortName = "")
    {
        var generatedPort = GeneratePort(dialogueNode, Direction.Output);

        var outputPortCount = dialogueNode.outputContainer.Query(name: "connector").ToList().Count;
        string outputPortName = $"Choice {outputPortCount}";

        var choicePortName = string.IsNullOrEmpty(overriddenPortName)
            ? $"Choice {outputPortCount + 1}"
            : overriddenPortName;

        var textFieldContainer = new VisualElement {
            style = {
                flexDirection = FlexDirection.Row,
                justifyContent = Justify.FlexEnd,
                alignItems = Align.FlexStart,
                overflow = Overflow.Visible,
                //backgroundColor = new StyleColor(new Color(245f / 255, 66f / 255, 224f / 255)) // za debug
            }
        };

        var deleteButton = new Button(() => RemovePort(dialogueNode, generatedPort))
        {
            text = "x"
        };
        textFieldContainer.Add(deleteButton);

        var textField = new TextField {
            name = string.Empty,
            value = choicePortName,
            style = {
                flexWrap = Wrap.Wrap,
                flexDirection = FlexDirection.Row,
                maxWidth = 200,
                minWidth = 50,
                whiteSpace = WhiteSpace.Normal
            }
        };
        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        textField.style.whiteSpace = WhiteSpace.Normal;
        textFieldContainer.Add(textField);


        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(oldLabel);

        generatedPort.contentContainer.Add(textFieldContainer);

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

    private void RemovePort(DialogueNode dialogueNode, Port generatedPort)
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
