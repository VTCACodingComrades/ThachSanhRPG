using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] Quest quest;

    public void GiveQuest()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuest>().SetQuest(quest);
    }
}
