using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CharacterInfoData {
    public string name;
    public int level;
    public float health;
    public float coin;

    public CharacterInfoData(string name, int levelPlayer, float health, float coin) {
        this.name = name;
        this.level = levelPlayer;
        this.health = health;
        this.coin = coin;
    }
}

public class PlayerData : Singleton<PlayerData>
{
    CharacterInfo characterInfo;

    private CharacterInfo ReturnClass() {
        return new CharacterInfo(name, 1, 
        PlayerHealth.Instance.CurrentHealth, PlayerCoin.Instance.CurrentBalance); // lay gia tri o cac class de luu healthInput.value
    }
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start() {
        Load();
    }
    private void Update() {
        //if(characterInfo == null) return;
        //SetUIPlayerData_String();
    }

    private void SetUI()
    {
        
    }

    IEnumerator DelaySetUIPlayerDataString() {
        yield return new WaitForSeconds(1f);
    }

    private void SetUIPlayerData_String() {
        StartCoroutine(DelaySetUIPlayerDataString());
        SetUI();
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

    public void Save() //todo khi trong game
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"playerName", PlayerCoin.Instance.CurrentName.ToString()},
                {"playerLevel", PlayerCoin.Instance.CurrentLevel.ToString()},// levelInput.value.ToString()
                {"playerHealth", PlayerHealth.Instance.CurrentHealth.ToString()}, //healthInput.value.ToString()
                {"playerCoin",   PlayerCoin.Instance.CurrentBalance.ToString()}, // cointInt.ToString()
            }
        },
        result => { Debug.Log("Player Data Title updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }
    public void Load()
    {
        PlayFabClientAPI.GetUserData(
            new GetUserDataRequest(),
            OnGetPlayerData_Loggin,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }
    private void OnGetPlayerData_Loggin(GetUserDataResult result) //todo nhan nut load Data player
    {
        Debug.Log("Received the following Player Data Title:");

        // if(result.Data != null && result.Data.ContainsKey("goldInt") && result.Data.ContainsKey("healthInt")) {
        //     CoinMananger.Instance.SetCurrentCoin(int.Parse(result.Data["goldInt"].Value));
        //     PlayerHealth.Instance.SetCurrentHealth(int.Parse(result.Data["healthInt"].Value));
        // }

        foreach (var eachData in result.Data)
        {
            switch (eachData.Key)
            {

                case "playerName":
                    PlayerCoin.Instance.SetCurrentName(result.Data[eachData.Key].Value);
                    Debug.Log(result.Data[eachData.Key].Value);
                    break;
                case "playerLevel":
                    PlayerCoin.Instance.SetCurrentLevel(int.Parse(result.Data[eachData.Key].Value));
                    break;
                case "playerHealth":
                    PlayerHealth.Instance.SetCurrentHealth(int.Parse(result.Data[eachData.Key].Value));
                    break;
                case "playerCoin":
                    PlayerCoin.Instance.SetCurrentBalance(float.Parse(result.Data[eachData.Key].Value));
                    
                    // if(int.Parse(result.Data[eachData.Key].Value) > 0) {
                    //     PlayerController.Instance.GetInventory().AddItem(new Item {itemScriptableObject = new ItemScriptableObject() {
                    //                                         itemType = Item.ItemType.Coin }, 
                    //                                         amount = int.Parse(result.Data[eachData.Key].Value)});
                    //}
                    break;
            }
        }
    }
    #endregion LUU DATA KIEU PLAYERDATA



    //todo 
}