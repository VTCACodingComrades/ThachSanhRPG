using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    [SerializeField] List<DialogueNode> nodes = new();
    Dictionary<string, DialogueNode> nodeLookup = new();

# if UNITY_EDITOR
    void Awake()
    {
        if (nodes.Count == 0)
        {
            DialogueNode rootNode = new();
            rootNode.uniqueId = Guid.NewGuid().ToString();
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
        //List<DialogueNode> result = new();
        foreach (string uniqueID in parentNode.childNode)
        {
            //result.Add(nodeLookup[uniqueID]);
            if(nodeLookup.ContainsKey(uniqueID))
                yield return nodeLookup[uniqueID];
        }
        //return result;
    }

    public void CreateNode(DialogueNode parentNode)
    {
        DialogueNode newNode = new();
        newNode.uniqueId = Guid.NewGuid().ToString();
        nodes.Add(newNode);
        parentNode.childNode.Add(newNode.uniqueId);
        nodeLookup.Add(newNode.uniqueId, newNode);
        OnValidate();
    }

    public void DeleteNode(DialogueNode deleteNode)
    {
        nodes.Remove(deleteNode);
        nodeLookup.Remove(deleteNode.uniqueId);
        OnValidate();
        foreach(DialogueNode node in GetAllNodes())
        {
            node.childNode.Remove(deleteNode.uniqueId);
        }
    }
}
