using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI objective;

    public void SetUp(Quest quest)
    {
        title.text = quest.GetTitle();
        objective.text = "0/" + quest.GetObjectiveNumber().ToString();
    }
}
