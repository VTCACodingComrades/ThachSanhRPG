using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] Quest quest;

    public void GiveQuest()
    {
        Debug.Log("Nhan nhiem vu ne player");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuest>().SetQuest(quest);
    }
}
