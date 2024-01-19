using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class DialogueEditor : EditorWindow
{
    [MenuItem("Window/Dialogue Editor")]
    public static void ShowEditorWindow()
    {
        GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
    }

    [OnOpenAsset(1)]
    public static bool OpenDialogue(int instanceID, int line)
    {
        //string name = EditorUtility.InstanceIDToObject(instanceID).name;
        //Debug.Log("Open Asset step: 1 (" + name + ")");
        Dialogue dialouge = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
        if (dialouge != null)
        {
            //Debug.Log("Open Dialogue");
            ShowEditorWindow();
            return true;
        }
        return false; // we did not handle the open
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Hello World");
        Repaint();
    }
}
