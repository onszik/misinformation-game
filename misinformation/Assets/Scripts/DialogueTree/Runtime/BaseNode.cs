using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Reflection;
public interface IBaseNode {
    public string GUID
    {
        get; set;
    }
    public string nodeType
    {
        get; set;
    }
}

public class NodeData {
    public string GUID;
    public string nodeType;
    public Vector2 position;

    public Dictionary<string, object> properties = new Dictionary<string, object>();
}

public class DialogueNode : Node, IBaseNode {
    private string _guid;
    private string _type;
    public string GUID { get { return _guid; } set { _guid = value; } }
    public string nodeType { get { return _type; } set { _type = value; } }

    public bool entryPoint;

    public string dialogueText;
}

public class TweetNode : Node, IBaseNode {
    private string _guid;
    private string _type;
    public string GUID { get { return _guid; } set { _guid = value; } }
    public string nodeType { get { return _type; } set { _type = value; } }

    public string username;
    public string dialogueText;
    public int value;


}
public class ExecNode : Node, IBaseNode {
    private string _guid;
    private string _type;
    public string GUID { get { return _guid; } set { _guid = value; } }
    public string nodeType { get { return _type; } set { _type = value; } }


    public Action action;
}
public class NodeDataManager {
    private List<NodeData> nodeDataList = new List<NodeData>();

    public void AddNodeData(IBaseNode node)
    {
        NodeData nodeData = new NodeData
        {
            GUID = node.GUID,
            nodeType = node.nodeType
        };

        var derivedType = node.GetType();

        foreach (var property in derivedType.GetProperties())
        {
            if (property.DeclaringType == derivedType && property.CanRead)
            {
                object value = property.GetValue(node);
                if (value != null)
                {
                    nodeData.properties.Add(property.Name, value);
                }
            }
        }

        foreach (var field in derivedType.GetFields())
        {
            if (field.DeclaringType == derivedType && field.IsPublic)
            {
                object value = field.GetValue(node);
                if (value != null)
                {
                    nodeData.properties.Add(field.Name, value);
                }
            }
        }

        nodeDataList.Add(nodeData);
    }

    public NodeData GetNodeData(string GUID)
    {
        return nodeDataList.Find(data => data.GUID == GUID);
    }

    public List<NodeData> GetAllNodeData()
    {
        return nodeDataList;
    }
}