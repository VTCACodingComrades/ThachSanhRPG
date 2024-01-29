using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI objective;
    Quest quest;

    public void SetUp(Quest quest)
    {
        this.quest = quest;
        title.text = quest.GetTitle();
        objective.text = "0/" + quest.GetObjectiveNumber().ToString();
    }

    public Quest GetQuest()
    {
        return quest;
    }
}
