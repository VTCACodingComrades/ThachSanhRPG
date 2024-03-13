using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class DialogueNode : ScriptableObject
{
    [SerializeField]
    string speakerName;
    [SerializeField]
    bool isPlayerSpeaking = false;
    [SerializeField]
    string text;
    [SerializeField]
    List<string> children = new List<string>();
    [SerializeField]
    Rect rect = new Rect(0, 0, 200, 100);
    [SerializeField] string enterNodeAction;
    [SerializeField] string exitNodeAction;

    public Rect GetRect()
    {
        return rect;
    }

    public string GetText()
    {
        return text;
    }

    public List<string> GetChildren()
    {
        return children;
    }

    public string GetExitAction()
    {
        return exitNodeAction;
    }

#if UNITY_EDITOR
    public void SetPosition(Vector2 newPosition)
    {
        Undo.RecordObject(this, "Move Dialogue Node");
        rect.position = newPosition;
        EditorUtility.SetDirty(this);
    }

    public void SetText(string newText)
    {
        if (newText != text)
        {
            Undo.RecordObject(this, "Update Dialogue Text");
            text = newText;
        }
        EditorUtility.SetDirty(this);
    }

    public void AddChild(string childID)
    {
        Undo.RecordObject(this, "Add Dialogue Link");
        children.Add(childID);
        EditorUtility.SetDirty(this);
    }

    public void RemoveChild(string childID)
    {
        Undo.RecordObject(this, "Remove Dialogue Link");
        children.Remove(childID);
        EditorUtility.SetDirty(this);
    }

   

    public void SetSpeakerText(string newText)
    {
        if (newText != text)
        {
            Undo.RecordObject(this, "Update Dialogue Text");
            speakerName = newText;
        }
        EditorUtility.SetDirty(this);
    }
    #endif

    public bool IsPlayerSpeaking()
    {
        return isPlayerSpeaking;
    }

    internal void MakeIsPlayerSpeaking(bool newIsPlayerSpeaking)
    {
        isPlayerSpeaking = newIsPlayerSpeaking;
    }

    public string GetSpeakerText()
    {
        return speakerName;
    }
}