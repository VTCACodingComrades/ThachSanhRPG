using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class DialogueEditor : EditorWindow
{
    Dialogue selectedDialogue;

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
            //EditorGUILayout.LabelField(selectedDialogue.name);
            foreach(DialogueNode item in selectedDialogue.GetAllNodes())
            {
                //EditorGUILayout.LabelField(item.text);
                item.text = EditorGUILayout.TextField(item.text);
            }
        }
        Repaint();
    }

    
}
