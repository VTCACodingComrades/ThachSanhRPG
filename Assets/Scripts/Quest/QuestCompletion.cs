using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletion : MonoBehaviour
{
    [SerializeField] Quest quest;
    [SerializeField] string objective;

    public void CompleteObjective()
    {
        PlayerQuest playerQuest = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuest>();
        playerQuest.CompleteObjective(quest, objective);
    }

    public void GiveReward()
    {
        PlayerQuest playerQuest = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuest>();
        playerQuest.GiveRewards(quest);
    }
}
