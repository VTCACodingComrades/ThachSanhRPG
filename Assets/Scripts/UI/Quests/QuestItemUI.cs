using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI objective;
    QuestStatus status;

    public void SetUp(QuestStatus status)
    {
        this.status = status;
        title.text = status.GetQuest().GetTitle();
        objective.text = status.GetCompletedObjectiveQuantity().ToString() + "/" + status.GetQuest().GetObjectiveNumber().ToString();
    }

    public QuestStatus GetQuestStatus()
    {
        return status;
    }
}
