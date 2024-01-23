using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    PlayerConversant playerConversant;
    [SerializeField] TextMeshProUGUI textAI;

    private void Start()
    {
        playerConversant = GameObject.Find("Player").GetComponent<PlayerConversant>();
        textAI.text = playerConversant.GetText();
    }
}
