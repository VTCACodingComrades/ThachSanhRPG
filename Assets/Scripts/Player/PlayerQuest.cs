using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    [SerializeField] QuestStatus[] questsStatus;

    internal IEnumerable<QuestStatus> GetQuest()
    {

        return questsStatus;

    }
}
