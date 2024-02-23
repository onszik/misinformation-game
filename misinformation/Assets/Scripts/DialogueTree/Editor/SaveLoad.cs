using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System.Linq;
using System;

public class SaveLoad : MonoBehaviour 
{
    private DialogueGraphView _targetGraphView;
    private DialogueContainer _containerCache;
    private NodeDataManager _nodeDataManager;

    private void Start()
    {
        var temp = new NodeDataManager();
    }

    public static SaveLoad GetInstance(DialogueGraphView targetGraphView)
    {
        return new SaveLoad
        {
            _targetGraphView = targetGraphView,
            _nodeDataManager = new NodeDataManager()
        };
    }

    public void SaveGraph(string fileName)
    {
        if (!_targetGraphView.edges.Any())
            return;

        DialogueContainer dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

        foreach (var edge in _targetGraphView.edges.ToList())
        {
            var inputNode = edge.input.node as IBaseNode;
            var outputNode = edge.output.node as IBaseNode;

            if (inputNode != null && outputNode != null)
            {
                dialogueContainer.nodeLinks.Add(new NodeLinkData
                {
                    baseNodeGUID = outputNode.GUID,
                    portName = edge.output.portName,
                    targetNodeGUID = inputNode.GUID
                });
            }
        }

        foreach (var node in _targetGraphView.nodes.ToList())
        {
            var baseNode = node as IBaseNode;
            if (baseNode != null)
            {
                _nodeDataManager.AddNodeData(baseNode);
            }
        }

        dialogueContainer.nodeData = _nodeDataManager.GetAllNodeData();

        AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<DialogueContainer>(fileName);
        if (_containerCache == null)
        {
            EditorUtility.DisplayDialog("File not found", "No file with the specified name exists.", "Continue");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void ClearGraph()
    {
        _targetGraphView.nodes.ToList().ForEach(node =>
        {
            if (node is DialogueNode && (node as DialogueNode).entryPoint)
                return;

            _targetGraphView.RemoveElement(node);
        });
        _targetGraphView.edges.ToList().ForEach(node =>
        {
            _targetGraphView.RemoveElement(node);
        });
    }

    private void CreateNodes()
    {
        foreach (var nodeData in _containerCache.nodeData)
        {
            // Create a new node based on its node type
            Node tempNode = _targetGraphView.CreateNodeOfType(nodeData.nodeType, nodeData.nodeType);

            // Set common properties
            (tempNode as IBaseNode).GUID = nodeData.GUID;
            tempNode.SetPosition(new Rect(nodeData.position, _targetGraphView.defaultNodeSize));

            // Set node-specific properties using reflection
            var properties = tempNode.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (nodeData.properties.ContainsKey(property.Name))
                {
                    var value = nodeData.properties[property.Name];
                    // Convert the value to the property's type
                    if (property.PropertyType == typeof(int)) 
                    {
                        property.SetValue(tempNode, (int)value);
                    } 
                    else if (property.PropertyType == typeof(string)) 
                    {
                        property.SetValue(tempNode, (string)value);
                    } 
                    else if (property.PropertyType == typeof(bool)) 
                    {
                        property.SetValue(tempNode, (bool)value);
                    }
                }
            }

            // Same for fields
            var fields = tempNode.GetType().GetFields();
            foreach (var field in fields)
            {
                if (nodeData.properties.ContainsKey(field.Name))
                {
                    var value = nodeData.properties[field.Name];
                    // Convert the value to the field's type
                    if (field.FieldType == typeof(int))
                    {
                        field.SetValue(tempNode, (int)value);
                    }
                    else if (field.FieldType == typeof(string))
                    {
                        field.SetValue(tempNode, (string)value);
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        field.SetValue(tempNode, (bool)value);
                    }
                }
            }

            var nodePorts = _containerCache.nodeLinks.Where(x => x.baseNodeGUID == nodeData.GUID).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.portName));

            // Add the node to the graph view
            _targetGraphView.AddElement(tempNode);
        }
    }

    private void ConnectNodes()
    {
        foreach (var link in _containerCache.nodeLinks)
        {
            var inputNode = _targetGraphView.nodes.ToList().FirstOrDefault(node => (node as IBaseNode)?.GUID == link.targetNodeGUID);
            var outputNode = _targetGraphView.nodes.ToList().FirstOrDefault(node => (node as IBaseNode)?.GUID == link.baseNodeGUID);
            if (inputNode != null && outputNode != null)
            {
                var inputPort = inputNode.inputContainer.Children().First(port => port.name == link.portName) as Port;
                var outputPort = outputNode.outputContainer.Children().First(port => port.name == link.portName) as Port;

                if (inputPort != null && outputPort != null)
                {
                    LinkNodes(outputPort, inputPort);
                }
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
