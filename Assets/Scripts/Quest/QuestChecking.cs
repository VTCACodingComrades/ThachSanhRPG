using RPG.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChecking : MonoBehaviour
{
    [SerializeField] string objective;
    [SerializeField] Dialogue newDialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        GameObject[] objectives = GameObject.FindGameObjectsWithTag(objective);
        if (objectives.Length == 0)
        {
            gameObject.GetComponentInParent<AIConversant>().SetDialogue(newDialogue);
            StopChecking();
        }
    }

    public void StartChecking()
    {
        gameObject.SetActive(true);
    }

    public void StopChecking()
    {
        gameObject.SetActive(false);
    }
}
