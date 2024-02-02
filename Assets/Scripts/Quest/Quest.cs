using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest", order = 0)]
public class Quest : ScriptableObject
{
    [SerializeField] List<Objective> objectives = new List<Objective>();
    [SerializeField] List<Reward> rewards = new List<Reward>();

    [System.Serializable]
    class Reward
    {
        public int number;
        public Item item;
    }

    [System.Serializable]
    public class Objective
    {
        public string reference;
        public string description;
    }

    public string GetTitle()
    {
        return name;
    }

    public int GetObjectiveNumber()
    {
        return objectives.Count;
    }

    public Objective GetObjective(int index)
    {
        return objectives[index];
    }

    public bool HasObjective(string objectiveRef)
    {
        foreach(var objective in objectives)
        {
            if (objectiveRef == objective.reference)
            {
                return true;
            }
        }
        return false; 
        //return objectives.Contains(objective);
    }
}

