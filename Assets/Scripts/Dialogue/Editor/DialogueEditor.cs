using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class DialogueEditor : EditorWindow
{
    Dialogue selectedDialogue;
    GUIStyle nodeStyle;
    private bool dragging = false;
    DialogueNode draggingNode = null;
    private Vector2 offsetPosition;

    [MenuItem("Window/Dialogue Editor")]
    public static void ShowEditorWindow()
    {
        GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
    }

    [OnOpenAsset(1)]
    public static bool OpenDialogue(int instanceID, int line)
    {
        Dialogue dialouge = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
        if (dialouge != null)
        {
            //Debug.Log("Open Dialogue");
            ShowEditorWindow();
            return true;
        }
        return false; // we did not handle the open
    }

    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectionChanged;
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
        nodeStyle.padding = new RectOffset(20, 20, 20, 20);
        nodeStyle.border = new RectOffset(20, 20, 20, 20);
    }

    private void OnSelectionChanged()
    {
        //Debug.Log("On Selection Change");
        Dialogue newDialogue = Selection.activeObject as Dialogue;
        if (newDialogue != null)
        {
            selectedDialogue = newDialogue;
        }     
    }

    private void OnGUI()
    {
        if (selectedDialogue == null)
        {
            EditorGUILayout.LabelField("No dialogue selected");
        }
        else
        {
            ProcessEvent();
            foreach(DialogueNode item in selectedDialogue.GetAllNodes())
            {
                OnGUINode(item);
            }
        }
        Repaint();
    }

    private void ProcessEvent()
    {
        if (Event.current.type == EventType.MouseDown && draggingNode == null)
        {
            draggingNode = GetNodeAtPoint(Event.current.mousePosition);
            if (draggingNode != null)
                offsetPosition = draggingNode.rect.position - Event.current.mousePosition;
        }
        else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
        {
            Undo.RecordObject(selectedDialogue, "Move Dialogue Node");
            draggingNode.rect.position = Event.current.mousePosition + offsetPosition;
            GUI.changed = true;
        }    
        else if (Event.current.type == EventType.MouseUp && draggingNode != null)
        {
            draggingNode = null;
        }
    }

    private DialogueNode GetNodeAtPoint(Vector2 point)
    {
        DialogueNode foundNode = null;
        foreach(DialogueNode node in selectedDialogue.GetAllNodes())
        {
            if (node.rect.Contains(point))
                foundNode = node;
        }
        return foundNode;
    }

    private void OnGUINode(DialogueNode item)
    {
        GUILayout.BeginArea(item.rect, nodeStyle);
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.LabelField("Node:");
        string newText = EditorGUILayout.TextField(item.text);
        string newIdText = EditorGUILayout.TextField(item.uniqueId);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(selectedDialogue, "Update Dialogue");
            item.text = newText;
            item.uniqueId = newIdText;
        }
        GUILayout.EndArea();
    }

}
