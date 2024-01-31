using RPG.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIConversant : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Noi chuyen ne");
            collision.GetComponent<PlayerConversant>().StartConversant(dialogue, this);
        }
    }
}
