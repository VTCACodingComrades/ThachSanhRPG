using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
    //[SerializeField] Quest[] quests;
    [SerializeField] QuestItemUI questItemUIPrefab;

    private void Start()
    {
        PlayerQuest playerQuest = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuest>();
        transform.DetachChildren();
        foreach(QuestStatus questStatus in playerQuest.GetQuest())
        {
            QuestItemUI questItemUI = Instantiate<QuestItemUI>(questItemUIPrefab, transform);
            questItemUI.SetUp(questStatus);
        }
    }
}
