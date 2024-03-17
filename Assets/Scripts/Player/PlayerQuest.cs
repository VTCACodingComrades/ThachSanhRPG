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
        if (questsStatus.Any(q => q.GetQuest().GetTitle() == quest.GetTitle()))
        {
            return;
        }
        QuestStatus questStatus = new QuestStatus(quest);
        questsStatus.Add(questStatus);
        OnQuestStatusUpdate.Invoke();
    }

    public void CompleteObjective(Quest quest, string objective)
    {
        QuestStatus questStatus = GetQuestStatus(quest);
        questStatus.CompleteObjective(objective);
        //if (questStatus.IsComplete())
        //{
        //    GiveRewards(quest);
        //}
        OnQuestStatusUpdate.Invoke();
    }

    public void GiveRewards(Quest quest)
    {
        Inventory playerInventory = GetComponent<PlayerController>().GetPlayerInventory();
        foreach (var reward in quest.GetRewards())
        {
            if (reward.item.itemScriptableObject.itemType == Item.ItemType.Coin)
            {
                Purse shopperPurse = GameObject.FindGameObjectWithTag("Player").GetComponent<Purse>();
                shopperPurse.UpdateBalance(reward.item.amount);
            }
            else
            {
                playerInventory.AddItem(reward.item);
            }         
        }
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
