using RPG.Dialogue;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestChecking : MonoBehaviour
{
    [SerializeField] string objective;
    //[SerializeField] Dialogue newDialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> objectives = GameObject.FindGameObjectsWithTag(objective).ToList();
        if (objectives.Count == 0)
        {
            gameObject.GetComponentInParent<AIConversant>().SetNextDialogue();
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
