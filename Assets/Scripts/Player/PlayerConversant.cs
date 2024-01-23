using RPG.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConversant : MonoBehaviour
{
    [SerializeField] Dialogue currentDialogue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetText()
    {
        if (currentDialogue != null)
        {
            return currentDialogue.GetRootNode().GetText();
        }
        else
            return "";
    }
}
