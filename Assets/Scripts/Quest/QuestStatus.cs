using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class QuestStatus
{
    [SerializeField] Quest quest;
    [SerializeField] string[] completedObjective;

    public Quest GetQuest()
    {
        return quest;
    }

    public bool IsObjectiveComplete(string objective)
    {
        return completedObjective.Contains(objective);
    }

    public int GetCompletedObjectiveQuantity()
    {
        return completedObjective.Length;
    }
}
