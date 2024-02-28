using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CharacterInfo {
    public string name;
    public float level;
    public float health;
    public int coin;

    public CharacterInfo(string name, float levelPlayer, float health, int coin) {
        this.name = name;
        this.level = levelPlayer;
        this.health = health;
        this.coin = coin;
    }
}

public class PlayerData_Loggin : MonoBehaviour
{
    CharacterInfo characterInfo;
    [SerializeField] Button loggin_Button;
    [SerializeField] GameObject levelsButton_GO;
    [SerializeField] GameObject logginButton_GO;

    
    [SerializeField] public TMP_InputField nameInput;
    [SerializeField] private Slider levelInput;
    [SerializeField] private Slider healthInput;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private bool isRegistered;

    public bool GetIsRegisteredID_Data{get {return isRegistered;}}

    private CharacterInfo ReturnClass() {
        return new CharacterInfo(nameInput.text, levelInput.value, 
        PlayerHealth.Instance.CurrentHealth, PlayerCoin.Instance.CurrentCoin); // lay gia tri o cac class de luu healthInput.value
    }
    private void Start() {
        // cointInt = 0;
        // coinText.text = "Coin: " + cointInt.ToString("D3");
        levelsButton_GO.SetActive(false);
        LoadPlayerData_Loggin();
        
    }
    private void Update() {
        // if(characterInfo == null) return;
        // SetUIPlayerData_String();
        //? neu da co ten khi load thi ko hien login hien level
        if(isRegistered) {
            logginButton_GO.SetActive(false);
            levelsButton_GO.SetActive(true);
        } else {
            logginButton_GO.SetActive(true);
            levelsButton_GO.SetActive(false);
        }
        
        // //?dieu kien nhap ten > 5 ki tu se cho nhan loggin
        if(nameInput.text.Length > 5) {
            loggin_Button.enabled = true;
        } 
        else {
            loggin_Button.enabled = false;
        }
        
        // //? neu nhan loggin va success thi hien hut levels
        // if(isLoginedID_Data) {
        //     levelsButton_GO.SetActive(true);
        // } 
        // else levelsButton_GO.SetActive(false);

        
    }

    private void SetUI()
    {
        nameInput.text = characterInfo.name;
        levelInput.value = characterInfo.level;
        healthInput.value = characterInfo.health;
        coinText.text = characterInfo.coin.ToString();
    }

    IEnumerator DelaySetUIPlayerDataString() {
        yield return new WaitForSeconds(1f);
    }
    private void SetUIPlayerData_String() {
        StartCoroutine(DelaySetUIPlayerDataString());
        //SetUI();
    }

    #region LUU DATA KIEU PLAYERDATA
    private void OnDataLoadSuccess(GetUserDataResult result)
    {
        // Handle loaded player data
        foreach (var entry in result.Data)
        {
            Debug.Log("Key: " + entry.Key + ", Value: " + entry.Value.Value);
        }
    }
    private void OnDataLoadFailure(PlayFabError error) => Debug.LogError("Player data load failed: " + error.ErrorMessage);
    public void LoadPlayerData() =>PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataLoadSuccess, OnDataLoadFailure);

    public void SavePlayerData_Loggin() //todo nhan nut luu data player
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"playerName", nameInput.text.ToString()},
                {"playerLevel", "1"},// levelInput.value.ToString()
                {"playerHealth", "10"}, //healthInput.value.ToString()
                {"playerCoin",   "0"}, // cointInt.ToString()
            }
        },
        result => { Debug.Log("Player Data Title updated");},
        error => { Debug.LogError(error.GenerateErrorReport());});

        StartCoroutine(DelayTimeSaveLoadeCountine());
    }
    public void LoadPlayerData_Loggin()
    {
        PlayFabClientAPI.GetUserData(
            new GetUserDataRequest(),
            OnGetPlayerData_Loggin,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    private void OnGetPlayerData_Loggin(GetUserDataResult result) //todo nhan nut load Data player
    {
        //if(result.Data == null) return;
        // if(result.Data != null && result.Data.ContainsKey("goldInt") && result.Data.ContainsKey("healthInt")) {
        //     CoinMananger.Instance.SetCurrentCoin(int.Parse(result.Data["goldInt"].Value));
        //     PlayerHealth.Instance.SetCurrentHealth(int.Parse(result.Data["healthInt"].Value));
        // }

        foreach (var eachData in result.Data)
        {
            switch (eachData.Key)
            {
                case "playerName":
                    nameInput.text = result.Data[eachData.Key].Value; // hien thi ui tai day

                    if(result.Data["playerName"].Value != null) {
                    Debug.Log("Da dang ky");
                    isRegistered = true;
                    } else {
                        Debug.Log("Chua dang ky");
                        isRegistered = false;
                    }
                    break;
                case "playerLevel":
                    if(result.Data[eachData.Key].Value == null) levelInput.value = 0;
                    else levelInput.value = int.Parse(result.Data[eachData.Key].Value);
    
                    levelInput.GetComponentInChildren<TMP_Text>().text = "Level: " + result.Data[eachData.Key].Value;
                    break;
                case "playerHealth":
                    if(result.Data[eachData.Key].Value == null) healthInput.value = 0;
                    else healthInput.value = int.Parse(result.Data[eachData.Key].Value);
                    
                    healthInput.GetComponentInChildren<TMP_Text>().text = "Health: " + result.Data[eachData.Key].Value;
                    break;
                case "playerCoin":
                    coinText.GetComponent<TMP_Text>().text = "Coin: " + result.Data[eachData.Key].Value;
                    break;
            }
        }
    }
    #endregion LUU DATA KIEU PLAYERDATA

    IEnumerator DelayTimeSaveLoadeCountine() {
        yield return new WaitForSeconds(2f);
        LoadPlayerData_Loggin();
    }



    //todo 
}