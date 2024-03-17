using RPG.Dialogue;
using RPGame.Saving;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class QuestChecking : MonoBehaviour, ISaveable
{
    [SerializeField] string objective;
    //[SerializeField] Dialogue newDialogue;
    // Start is called before the first frame update
    public UnityEvent OnQuestComplete;
    bool isCheck = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCheck) return;    
        List<GameObject> objectives = GameObject.FindGameObjectsWithTag(objective).ToList();
        Debug.Log(objectives.Count);
        if (objectives.Count == 0)
        {
            gameObject.GetComponentInParent<AIConversant>().SetNextDialogue();
            gameObject.GetComponentInParent<QuestCompletion>().CompleteObjective();
            OnQuestComplete?.Invoke();
            StopChecking();
        }
    }

    public void StartChecking()
    {
        //gameObject.SetActive(true);
        isCheck = true;
    }

    public void StopChecking()
    {
        //gameObject.SetActive(false);
        isCheck = false;
    }

    public object CaptureState()
    {
        return isCheck;
    }

    public void RestoreState(object state)
    {
        isCheck = (bool) state;
    }
}
