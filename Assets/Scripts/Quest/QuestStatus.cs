using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class QuestStatus
{
    [SerializeField] Quest quest;
    [SerializeField] List<string> completedObjective = null;

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
        if (completedObjective == null) return false;
        return completedObjective.Contains(objective);
    }

    public int GetCompletedObjectiveQuantity()
    {
        if (completedObjective == null) return 0;
        return completedObjective.Count();
    }
}
