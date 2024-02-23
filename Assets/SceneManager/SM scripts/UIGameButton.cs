using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;


public class UIGameButton : MonoBehaviour
{
    //? Gameobject = panel nut nhan ben trong scene moi level
    private GameObject inventoryPanel; //panel chua item
    [SerializeField] private GameObject backQuitPanel; // panel hien len khi muon quit or back mainMenu

    private float delayTimeUIButtonPressed = 0.2f;
    private void Awake() {
        inventoryPanel = GameObject.Find("UI_Inventory_Go");
        //backQuitPanel = GameObject.Find("BackQuit_Panel");

    }
    private void Start() {
        //inventoryPanel.SetActive(false);
        backQuitPanel.SetActive(false);

    }

    public void ShowingInventory_Button() //? hien thi inventory khi dang choi
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void Showing_BackQuitButton() //? nut nhan quay lai mainmenu khi dang choi
    {
        backQuitPanel.SetActive(!backQuitPanel.activeSelf);
    }

    public void BackToMainMenuButton() //? quay lai mainmenu khi dang choi
    {
        // PlayfabManager.Instance.SendLeaderBoard(CoinMananger.Instance.CurrentCoin); // Gold Leaderboard
        // PlayfabManager.Instance.SavePlayerDataInt();// luu playerData

        StartCoroutine(SwitchUIGameButtonRoutine());
        SceneManagement.Instance.BackToMainMenu();
        Time.timeScale = 0; // frezze
    }

    public void QuitGameButton() //? thoat khi dang choi
    {
        // PlayfabManager.Instance.SendLeaderBoard(CoinMananger.Instance.CurrentCoin); // Gold Leaderboard
        // PlayfabManager.Instance.SavePlayerDataInt();// luu playerData

        StartCoroutine(SwitchUIGameButtonRoutine());
        SceneManagement.Instance.ExitGame();
    }
    IEnumerator SwitchUIGameButtonRoutine() {
        yield return new WaitForSeconds(delayTimeUIButtonPressed);
    }
}
