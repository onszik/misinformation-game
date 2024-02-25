using System;
using UnityEngine;

namespace Subtegral.DialogueSystem.DataContainers
{
    [Serializable]
    public class DialogueNodeData
    {
        public string NodeGUID;
        public string DialogueText;
        public Vector2 Position;

        public bool IsTweetNode = false;
        public string Username;
        public int Value;
    }
}