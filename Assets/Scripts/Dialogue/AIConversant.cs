using RPG.Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIConversant : MonoBehaviour
{
    [SerializeField] Dialogue[] dialogue;
    private int currentDialogueIndex = -1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerConversant>().StartConversant(GetNextDialogue(), this);
        }
    }

    public Dialogue GetNextDialogue()
    {
        if (!HasNext()) return null;
        currentDialogueIndex += 1;
        return dialogue[currentDialogueIndex];
    }

    public void StartNextConversant()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>().StartConversant(GetNextDialogue(), this);
    }

    internal bool HasNext()
    {
        return currentDialogueIndex < dialogue.Length - 1;
    }
}
