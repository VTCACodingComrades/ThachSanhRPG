using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] Quest[] quests;
    [SerializeField] QuestItemUI questItemUIPrefab;

    private void Start()
    {
        transform.DetachChildren();
        foreach(Quest quest in quests)
        {
            QuestItemUI questItemUI = Instantiate<QuestItemUI>(questItemUIPrefab, transform);
            questItemUI.SetUp(quest);
        }
    }
}
