using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;


public class PlayFabManager : Singleton<PlayFabManager>
{
    protected override void Awake() {
        base.Awake();
    }
    void Start()
    {
        LoginWithDevice();
    }

    #region Login
    void LoginWithDevice()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }
    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successful login custom id/account create!");

        //GetTitleData(); // gui loi chao khi dang nhap thanh cong
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while loggin in/creating account");
        Debug.Log(error.GenerateErrorReport());
    }
    #endregion Login


}
