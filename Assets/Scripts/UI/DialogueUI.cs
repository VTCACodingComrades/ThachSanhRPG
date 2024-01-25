using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    PlayerConversant playerConversant;
    [SerializeField] TextMeshProUGUI speakerText;
    [SerializeField] TextMeshProUGUI textAI;
    [SerializeField] Button nextButton;
    [SerializeField] Button closeButton;
    [SerializeField] Transform choiceRoot;
    [SerializeField] GameObject choicePrefab;

    private void Start()
    {
        playerConversant = GameObject.Find("Player").GetComponent<PlayerConversant>();
        UpdateTextUI();
        
    }
    private void OnEnable()
    {
        nextButton.onClick.AddListener(NextButton);
        closeButton.onClick.AddListener(CloseButton);
    }

    private void OnDisable()
    {
        nextButton.onClick.RemoveListener(NextButton);
        closeButton.onClick.RemoveListener(CloseButton);
    }

    private void CloseButton()
    {
        gameObject.SetActive(false);
    }

    public void NextButton()
    {
        playerConversant.GetNextText();
        UpdateTextUI();
    }

    private void UpdateTextUI()
    {
        speakerText.text = playerConversant.GetSpeakerText();
        textAI.text = playerConversant.GetText();
        nextButton.gameObject.SetActive(playerConversant.HasNext());
        choiceRoot.DetachChildren();
        foreach(var item in playerConversant.GetChoice())
        {
            Instantiate(choicePrefab, choiceRoot);
            choicePrefab.GetComponentInChildren<TextMeshProUGUI>().text = item;
        }
    }
}
