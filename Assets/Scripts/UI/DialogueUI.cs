using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    PlayerConversant playerConversant;
    [SerializeField] TextMeshProUGUI textAI;
    [SerializeField] Button nextButton;

    private void Start()
    {
        playerConversant = GameObject.Find("Player").GetComponent<PlayerConversant>();
        UpdateTextUI();
        nextButton.onClick.AddListener(NextButton);
    }

   

    public void NextButton()
    {
        playerConversant.GetNextText();
        UpdateTextUI();
    }

    private void UpdateTextUI()
    {
        textAI.text = playerConversant.GetText();
        nextButton.gameObject.SetActive(playerConversant.HasNext());
    }
}
