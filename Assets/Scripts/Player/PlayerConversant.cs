using RPG.Dialogue;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerConversant : MonoBehaviour
{
    [SerializeField] Dialogue currentDialogue;
    DialogueNode currentNode;
    bool hasNext = true;
    void Start()
    {
        currentNode = currentDialogue.GetRootNode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetText()
    {
        if (currentDialogue != null)
        {
            return currentNode.GetText();
        }
        else
            return "";
    }

    public void GetNextText()
    {
        DialogueNode[] nodes = currentDialogue.GetAllChildren(currentNode).ToArray();
        if(nodes.Length != 0)
        {
            hasNext = true;
            int randomIndex = Random.Range(0, nodes.Length);
            currentNode = nodes[randomIndex];
        }
        {
            hasNext = false;
        }
    }

    public bool HasNext()
    {
        return hasNext;
    }
}
