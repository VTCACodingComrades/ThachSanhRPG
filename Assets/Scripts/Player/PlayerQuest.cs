using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    [SerializeField] List<QuestStatus> questsStatus;

    internal IEnumerable<QuestStatus> GetQuest()
    {

        return questsStatus;

    }

    public void SetQuest(Quest quest)
    {
        QuestStatus questStatus = new QuestStatus(quest);
        questsStatus.Add(questStatus);
    }
}
