using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class QuestStatus
{
    [SerializeField] Quest quest;
    [SerializeField] List<string> completedObjective = new List<string>();

    public QuestStatus(Quest quest)
    {
        this.quest = quest;
    }

    public Quest GetQuest()
    {
        return quest;
    }

    public bool IsObjectiveComplete(string objective)
    {
        //if (completedObjective == null) return false;
        return completedObjective.Contains(objective);

    }

    public int GetCompletedObjectiveQuantity()
    {
        if (completedObjective == null) return 0;
        return completedObjective.Count();
    }

    public void CompleteObjective(string objective)
    {
       if (quest.HasObjective(objective))
       {
           completedObjective.Add(objective);
       }
    }

    public List<string> GetObjectives()
    {
        return completedObjective;
    }

    internal bool IsComplete()
    {
        return quest.GetObjectiveNumber() == completedObjective.Count;
    }
}
