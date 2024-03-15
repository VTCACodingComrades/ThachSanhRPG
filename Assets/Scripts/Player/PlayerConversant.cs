using RPG.Dialogue;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerConversant : MonoBehaviour
{
    [SerializeField] Dialogue currentDialogue;
    AIConversant currentConversant;
    DialogueNode currentNode;
    [SerializeField] bool isChoose = false;
    public UnityEvent OnStartConversant;

    void Start()
    {
        //currentNode = currentDialogue.GetRootNode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartConversant(Dialogue dialogue, AIConversant conversant)
    {
        currentConversant = conversant;
        currentDialogue = dialogue;
        currentNode = currentDialogue.GetRootNode();
        OnStartConversant.Invoke();
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
        int numPlayerReponse = currentDialogue.GetPlayerChildren(currentNode).Count();
        if(numPlayerReponse > 0)
        {
            isChoose = true;
            return;
        }

        DialogueNode[] nodes = currentDialogue.GetAIChildren(currentNode).ToArray();
        if(nodes.Length != 0)
        {
            int randomIndex = Random.Range(0, nodes.Length);
            currentNode = nodes[randomIndex];
        }
    }

    public void SelectChoice(DialogueNode node)
    {
        currentNode = node;
        isChoose = false;
    }

    public bool HasNext()
    {
        //return hasNext;
        return currentNode.GetChildren().Count() != 0;
    }

    internal string GetSpeakerText()
    {
        if (currentDialogue != null)
        {
            return currentNode.GetSpeakerText();
        }
        else
            return "";
    }

    public IEnumerable<DialogueNode> GetChoice()
    {
        return currentDialogue.GetPlayerChildren(currentNode);
    }

    public bool IsChoose()
    {
        return isChoose;
    }

    public void Quit()
    {      
        OnExitAction();
        currentConversant = null;
        //currentDialogue = null;
        //currentNode = null;
        isChoose = false;
    }

    public void Close()
    {
        currentConversant = null;
    }

    public void StartNextConversant()
    {
        currentConversant.StartNextConversant();
    }

    public bool HasNextConversant()
    {
        return currentConversant.HasNext();
    }

    private void OnExitAction()
    {
        //DialogueTrigger dialogueTrigger = currentConversant.GetComponent<DialogueTrigger>();
        DialogueTrigger[] dialogueTriggers = currentConversant.GetComponents<DialogueTrigger>();
        foreach(DialogueTrigger dialogueTrigger in dialogueTriggers)
        {
            if (currentNode != null && currentNode.GetExitAction() != "")
            {
                dialogueTrigger.Trigger(currentNode.GetExitAction());
            }
        }
    }

}
