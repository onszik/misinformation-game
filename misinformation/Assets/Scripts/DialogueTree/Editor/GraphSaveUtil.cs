using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using System.Linq;

public class GraphSaveUtil : MonoBehaviour
{
    private DialogueGraphView _targetGraphView;
    private DialogueContainer _containerCache;

    private List<Edge> edges => _targetGraphView.edges.ToList();
    private List<DialogueNode> nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();

    public static GraphSaveUtil GetInstance(DialogueGraphView targetGraphView)
    {
        return new GraphSaveUtil
        {
            _targetGraphView = targetGraphView
        };
    }

    public void SaveGraph(string fileName)
    {
        if (!edges.Any())
            return;

        DialogueContainer dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

        var connectedPorts = edges.Where(x => x.input.node != null).ToArray();

        for (int i = 0; i < connectedPorts.Length; i++)
        {
            DialogueNode inputNode = connectedPorts[i].input.node as DialogueNode;
            DialogueNode outputNode = connectedPorts[i].output.node as DialogueNode;

            dialogueContainer.nodeLinks.Add(new NodeLinkData
            {
                baseNodeGUID = outputNode.GUID,
                portName = connectedPorts[i].output.portName,
                targetNodeGUID = inputNode.GUID
            });
        }

        foreach (DialogueNode n in nodes.Where(node => !node.entryPoint))
        {
            dialogueContainer.dialogueNodeData.Add(new DialogueNodeData
            {
                nodeGUID = n.GUID,
                dialogueText = n.dialogueText,
                position = n.GetPosition().position
            });
        }

        AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<DialogueContainer>(fileName);
        if (_containerCache == null) {
            EditorUtility.DisplayDialog("File not found", "No file with the specified name exists.", "Continue");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void ClearGraph()
    {
        nodes.Find(x => x.entryPoint).GUID = _containerCache.nodeLinks[0].baseNodeGUID;

        foreach (DialogueNode n in nodes)
        {
            if (n.entryPoint) continue;

            edges.Where(x => x.input.node == n).ToList()
                 .ForEach(edge => _targetGraphView.RemoveElement(edge));

            _targetGraphView.RemoveElement(n);
        }
    }

    private void CreateNodes()
    {
        foreach (var nodeData in _containerCache.dialogueNodeData)
        {
            var tempNode = _targetGraphView.CreateDialogueNode(nodeData.dialogueText);
            tempNode.GUID = nodeData.nodeGUID;
            _targetGraphView.AddElement(tempNode);

            var nodePorts = _containerCache.nodeLinks.Where(x => x.baseNodeGUID == nodeData.nodeGUID).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.portName));
        }
    }

    private void ConnectNodes()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            var connections = _containerCache.nodeLinks.Where(x => x.baseNodeGUID == nodes[i].GUID).ToList();

            for (int j = 0; j < connections.Count; j++)
            {
                var targetNodeGUID = connections[j].targetNodeGUID;
                var targetNode = nodes.First(x => x.GUID == targetNodeGUID);

                LinkNodes(nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(
                        _containerCache.dialogueNodeData.First(x => x.nodeGUID == targetNodeGUID).position,
                        _targetGraphView.defaultNodeSize
                ));
            }
        }
    }

    private void LinkNodes(Port output, Port input)
    {
        var tempEdge = new Edge
        {
            output = output,
            input = input
        };
        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);

        _targetGraphView.Add(tempEdge);
    }
}
