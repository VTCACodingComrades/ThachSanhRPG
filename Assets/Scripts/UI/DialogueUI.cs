using RPG.Dialogue;
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
    [SerializeField] Button okButton;
    [SerializeField] GameObject reponseAI;
    [SerializeField] Transform choiceRoot;
    [SerializeField] GameObject choicePrefab;
    [SerializeField] GameObject dialogueUI;

    private void Awake()
    {
        playerConversant = GameObject.Find("Player").GetComponent<PlayerConversant>();
        //UpdateTextUI();
        playerConversant.OnStartConversant.AddListener(UpdateTextUI);      
    }

    private void OnEnable()
    {
        nextButton.onClick.AddListener(NextButton);
        closeButton.onClick.AddListener(CloseButton);
        okButton.onClick.AddListener(OKButton);
    }

   
    private void OnDisable()
    {
        nextButton.onClick.RemoveListener(NextButton);
        closeButton.onClick.RemoveListener(CloseButton);
        okButton.onClick.RemoveListener(OKButton);
    }

    private void CloseButton()
    {
        playerConversant.Close();
        dialogueUI.SetActive(false);
        StopAllCoroutines();
    }

    private void OKButton()
    {
        playerConversant.Quit();
        dialogueUI.SetActive(false);
        StopAllCoroutines();
    }


    public void NextButton()
    {
        //Debug.Log("Click ne");
        playerConversant.GetNextText();
        UpdateTextUI();
    }

    private void UpdateTextUI()
    {
        dialogueUI.SetActive(true);
        reponseAI.SetActive(!playerConversant.IsChoose());
        choiceRoot.gameObject.SetActive(playerConversant.IsChoose());
        speakerText.text = playerConversant.GetSpeakerText();      
        
        if (playerConversant.IsChoose())
        {
            choiceRoot.DetachChildren();
            foreach (DialogueNode node in playerConversant.GetChoice())
            {
                speakerText.text = node.GetSpeakerText();
                GameObject choice = Instantiate(choicePrefab, choiceRoot);
                choice.GetComponentInChildren<TextMeshProUGUI>().text = node.GetText();
                choice.GetComponent<Button>().onClick.AddListener(() =>
                {
                    playerConversant.SelectChoice(node);
                    NextButton();
                });
            }
        }
        else
        {
            textAI.text = playerConversant.GetText();
            //nextButton.gameObject.SetActive(playerConversant.HasNext());
        }
        StartCoroutine(NextDialogue());
    }

    IEnumerator NextDialogue()
    {
        yield return new WaitForSeconds(2f);
        NextButton();
    }
}
