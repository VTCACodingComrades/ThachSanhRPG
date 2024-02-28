using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UILevelSelectButton : MonoBehaviour
{
    //! gameObject la panel chua nut chon level o mainmenu
    //! xet level hien tai de enable or Disable nut nhan Level

    //! khi chan FinishPoint object cuoi scene 1 => bien unLockLevelInt = indexScene +1 save len plafab

    [SerializeField] Button[] buttons;
    [SerializeField] Transform levelbuttons; // fill doi tuong con cua this.gameobject vao trong []

    public static UILevelSelectButton Instance;
    [SerializeField] private int unLockLevelInt;

    public int UnlockLevelInt {get{return unLockLevelInt;} set{unLockLevelInt = value;}}

    private void Awake() {

        UILevelSelectButton.Instance = this;

        //todo UILevelSelectButton.cs chi duoc run 1 lan duy nhat khi this.gameoject enable lan dau tien va gan gia tri
        //todo do do chi LoadLevel 1 lan dau tien khi this.gameObejct enable
        //todo muon LoadLevelInt thi phai tu scene kahc quay lai scene nay, de doi tuong chua this.cs enable lan nua

        LoadLevelInt("playerLevel");
        ButtonsToArray(); // keo nut nhan con vao torng transform doi tuong cha chua cac nut nhan
    }
    private void Start() {
        ChangeButtonLevelsStatus();
    }

    private void Update() {

        ChangeButtonLevelsStatus();
    }

//? tro ve level dau tien - xet level ve 1 - doi trang thai nut chon - save level
    public void ResetLevel()
    {
        unLockLevelInt = 1;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unLockLevelInt; i++)
        {
            buttons[i].interactable = true;
        }
        SaveLevelInt("playerLevel", unLockLevelInt);
    }

//? chuyen sence level khi nhan nut level
    public void OpenLevel(string nameIndex){
        string sceneName = "Scene_" + nameIndex;
        Time.timeScale = 1; //unfrezze trnh truong hop frezze dong 24 khi backtoMainMenu tu 1 scene nao do
        SceneManager.LoadScene(sceneName);
    }

//? keo transform child vao transform cha
    private void ButtonsToArray()
    {
        int childCount = levelbuttons.transform.childCount;
        buttons = new Button[childCount];

        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelbuttons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
    private void OnDataUpdateSuccess(UpdateUserDataResult result) => Debug.Log("Player data updated successfully!");
    private void OnDataLoadFailure(PlayFabError error) => Debug.LogError("Player data load failed: " + error.ErrorMessage);
    public void SaveLevelInt(string key, int value) {
        string stringValue = value.ToString();

        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string, string> {
                {key, stringValue}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataUpdateSuccess, OnDataLoadFailure);
    }
    
    public void LoadLevelInt(string key) //todo load khi Awake up() + khi nhan nut Levels o MeainMenu
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), 
            result =>
            {
                if (result.Data.TryGetValue(key, out var value))
                {
                    // Chuyển đổi từ chuỗi sang int khi tải
                    unLockLevelInt = int.Parse(value.Value);
                    
                    Debug.Log("level vua load xuong : " + unLockLevelInt);
                }
                else
                {
                    Debug.Log("Key not found.");
                }
            },
        OnDataLoadFailure);
    }

//? thay doi trang thai enable or disable levelSelect button dua theo bien unLockLevelInt
    public void ChangeButtonLevelsStatus()
    {
        StartCoroutine(ChangeStatusLevelSelectRountine());
    }
    IEnumerator ChangeStatusLevelSelectRountine()
    {
        yield return new WaitForSeconds(0);

        for (int i = 0; i < buttons.Length; i++) // hien tat
            buttons[i].interactable = false;

        for (int i = 0; i < unLockLevelInt; i++) // hien sang level hien tai
            buttons[i].interactable = true;
    }
}
