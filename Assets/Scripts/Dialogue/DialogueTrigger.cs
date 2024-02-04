using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] string actionTrigger;
    public UnityEvent OnDialogueTrigger;

    public void Trigger(string action)
    {
        if (action == actionTrigger)
        {
            OnDialogueTrigger.Invoke();
        }
    }
}
