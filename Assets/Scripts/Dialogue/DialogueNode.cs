using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string uniqueId;
    public string text;
    public string[] childNode;
    public Rect rect = new Rect(0, 0, 200, 100);
}
