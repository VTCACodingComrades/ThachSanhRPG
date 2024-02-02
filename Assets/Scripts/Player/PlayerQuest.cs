using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerQuest : MonoBehaviour
{
    [SerializeField] List<QuestStatus> questsStatus;
    public UnityEvent OnQuestStatusUpdate;

    internal IEnumerable<QuestStatus> GetQuest()
    {
        return questsStatus;
    }

    public void SetQuest(Quest quest)
    {
        QuestStatus questStatus = new QuestStatus(quest);
        questsStatus.Add(questStatus);
        OnQuestStatusUpdate.Invoke();
    }

    public void CompleteObjective(Quest quest, string objective)
    {
        //Debug.Log("Player hoan thanh nhiem vu ne");
        QuestStatus questStatus = GetQuestStatus(quest);
        //Debug.Log("Doc duoc quest status ne" + questStatus.GetQuest().name);
        questStatus.CompleteObjective(objective);
        OnQuestStatusUpdate.Invoke();
    }

    QuestStatus GetQuestStatus(Quest quest)
    {
        foreach (QuestStatus questStatus in questsStatus)
        {
            if (questStatus.GetQuest() == quest)
            {
                return questStatus;
            }
        }
        return null;
    }
}
