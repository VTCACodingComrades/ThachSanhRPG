using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string uniqueId;
    public string text;
    public List<string> childNode = new();
    public Rect rect = new(0, 0, 200, 100);
}
