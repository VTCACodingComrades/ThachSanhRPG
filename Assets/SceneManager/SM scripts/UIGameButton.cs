using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;


public class UIGameButton : MonoBehaviour
{
    //? Gameobject = panel nut nhan ben trong scene moi level
    private GameObject inventoryPanel;
    private float delayTimeUIButtonPressed = 0.2f;
    private void Awake() {
        inventoryPanel = GameObject.Find("UI_Inventory_Go");
    }
    private void Start() {
        inventoryPanel.SetActive(false);
    }
    public void InventoryButton() //? hien thi inventory khi dang choi
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
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
