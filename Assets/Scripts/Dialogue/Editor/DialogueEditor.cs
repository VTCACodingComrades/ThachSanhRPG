using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class DialogueEditor : EditorWindow
{
    Dialogue selectedDialogue;
    [NonSerialized] GUIStyle nodeStyle;
    [NonSerialized] DialogueNode draggingNode = null;
    [NonSerialized] private Vector2 offsetPosition;
    [NonSerialized] DialogueNode creatingNode = null;
    [NonSerialized] DialogueNode deleteNode = null;
    [NonSerialized] DialogueNode linkingParentNode = null;
    Vector2 scrollPosition;

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
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            GUILayoutUtility.GetRect(4000, 4000);
            foreach(DialogueNode node in selectedDialogue.GetAllNodes())
            {
                DrawNode(node);
            }
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                DrawConnections(node);
            }
            EditorGUILayout.EndScrollView();
            if(creatingNode != null)
            {
                Undo.RecordObject(selectedDialogue, "Create Dialogue Node");
                selectedDialogue.CreateNode(creatingNode);
                creatingNode = null;
            }
            if(deleteNode != null)
            {
                Undo.RecordObject(selectedDialogue, "Delete Dialogue Node");
                selectedDialogue.DeleteNode(deleteNode);
                deleteNode = null;
            }

        }
        //Repaint();
    }

    
    private void ProcessEvent()
    {
        if (Event.current.type == EventType.MouseDown && draggingNode == null)
        {
            draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
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

    private void DrawNode(DialogueNode node)
    {
        GUILayout.BeginArea(node.rect, nodeStyle);
        EditorGUI.BeginChangeCheck();
        //EditorGUILayout.LabelField("Node:");
        string newText = EditorGUILayout.TextField(node.text);
        //string newIdText = EditorGUILayout.TextField(node.uniqueId);
        //foreach (DialogueNode childNode in selectedDialogue.GetChildrenNode(node))
        //{
        //    //EditorGUILayout.TextField(childNode.text);
        //}
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(selectedDialogue, "Update Dialogue");
            node.text = newText;
            //node.uniqueId = newIdText;
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("x"))
        {
            Debug.Log("Delete node");
            deleteNode = node;
        }
        DrawLinkingNode(node);

        if (GUILayout.Button("+"))
        {
            Debug.Log("Create new node");
            creatingNode = node;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void DrawLinkingNode(DialogueNode node)
    {
        if (linkingParentNode == null)
        {
            if (GUILayout.Button("link"))
            {

                linkingParentNode = node;
            }
        }
        else if (node == linkingParentNode)
        {
            if (GUILayout.Button("cancel"))
            {
                linkingParentNode = null;
            }
        }
        else if (linkingParentNode.childNode.Contains(node.uniqueId))
        {
            if (GUILayout.Button("unlink"))
            {
                linkingParentNode.childNode.Remove(node.uniqueId);
                linkingParentNode = null;
            }
        }
        else
        {
            if (GUILayout.Button("child"))
            {
                Undo.RecordObject(selectedDialogue, "Linking Dialogue");
                linkingParentNode.childNode.Add(node.uniqueId);
                linkingParentNode = null;
            }
        }
    }

    private void DrawConnections(DialogueNode node)
    {
        Vector3 startPosition = new Vector2(node.rect.xMax, node.rect.center.y);
        foreach (DialogueNode childNode in selectedDialogue.GetChildrenNode(node))
        {        
            Vector3 endPosition = new Vector2(childNode.rect.xMin, childNode.rect.center.y);
            Vector3 offset =  endPosition - startPosition;
            offset.y = 0;
            offset.x *= 0.8f;
            Handles.DrawBezier(
                startPosition, endPosition,
                startPosition + offset,
                endPosition - offset, 
                Color.white, null, 4f);
        }    
      
    }


}
