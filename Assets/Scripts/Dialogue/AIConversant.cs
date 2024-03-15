using RPG.Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIConversant : MonoBehaviour
{
    [SerializeField] Dialogue[] dialogues;
    Dialogue currentDialogue;
    private int currentDialogueIndex = 0;

    private void Start()
    {
        currentDialogue = dialogues[0];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerConversant>().StartConversant(currentDialogue, this);
        }
    }

    //public void SetDialogue(Dialogue newDialogue)
    //{
    //    dialogue = newDialogue;
    //}
    //public Dialogue GetNextDialogue()
    //{
    //    if (!HasNext()) return null;
    //    currentDialogueIndex += 1;
    //    return dialogue[currentDialogueIndex];
    //}

    //public void StartNextConversant()
    //{
    //    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>().StartConversant(GetNextDialogue(), this);
    //}

    public void SetNextDialogue()
    {
        if (!HasNext()) return;
        currentDialogueIndex += 1;
        currentDialogue =  dialogues[currentDialogueIndex];
    }

    public bool HasNext()
    {
        return currentDialogueIndex < dialogues.Length - 1;
    }
}
