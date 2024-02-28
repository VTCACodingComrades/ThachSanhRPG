
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
        name = result.PlayFabId.ToString();
        Debug.Log("Login with ID Customer" + name);

    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while loggin in/creating account");
        Debug.Log(error.GenerateErrorReport());
    }

    #endregion Login

}
