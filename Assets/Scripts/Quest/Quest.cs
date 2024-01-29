using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest", order = 0)]
public class Quest : ScriptableObject
{
    [SerializeField] string[] objectives;

    public string GetTitle()
    {
        return name;
    }

    public int GetObjectiveNumber()
    {
        return objectives.Length;
    }

    public string GetObjective(int index)
    {
        return objectives[index];
    }
}

