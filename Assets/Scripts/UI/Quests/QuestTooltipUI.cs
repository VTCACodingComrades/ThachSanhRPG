using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestTooltipUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] Transform objectivesContainer;
    [SerializeField] GameObject objectivePrefab;
    public void Setup(Quest quest)
    {
        objectivesContainer.DetachChildren();
        title.text = quest.GetTitle();
        for(int i = 0; i < quest.GetObjectiveNumber(); i++)
        {
            GameObject objectiveObject = Instantiate(objectivePrefab, objectivesContainer);
            objectiveObject.GetComponentInChildren<TextMeshProUGUI>().text = quest.GetObjective(i);
        }
    }
}
