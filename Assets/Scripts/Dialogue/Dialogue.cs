using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    [SerializeField] List<DialogueNode> nodes;
    Dictionary<string, DialogueNode> nodeLookup = new();

# if UNITY_EDITOR
    void Awake()
    {
        if (nodes.Count == 0)
        {
            nodes.Add(new DialogueNode());
        }
    }
# endif

    private void OnValidate()
    {
        nodeLookup.Clear();
        foreach(DialogueNode node in GetAllNodes())
        {
            nodeLookup[node.uniqueId] = node;
        }
    }

    public IEnumerable<DialogueNode> GetAllNodes()
    {
        return nodes;
    }

    public DialogueNode GetRootNode()
    {
        return nodes[0];
    }

    public IEnumerable<DialogueNode> GetChildrenNode(DialogueNode parentNode)
    {
        List<DialogueNode> result = new();
        foreach (string uniqueID in parentNode.childNode)
        {
            result.Add(nodeLookup[uniqueID]);
        }
        return result;
    }
}
