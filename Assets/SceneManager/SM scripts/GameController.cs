using RPGame.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : Singleton<GameController>
{
    //? gameobject = doi tuong  trong UIGame trong moi scene singleton
    //? dung de instantiate enemy
    [SerializeField] private GameObject gameOverPanel;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start() {

        CameraController.Instance.SetPlayerCameraFollow();
        // PlayfabManager.Instance.LoadPlayerDataInt(); //? load data playerData theo case
        
    }
    private void Update() {

        // CoinMananger.Instance.ShowCoinAfterLoadingCoidData();
    }

    public void GameOverSendData() //? khi playerhealth < 0 se goi ham nay de luu diem + hieb panel
    {
        // PlayfabManager.Instance.SavePlayerDataInt(); //todo dave data player(int data)
        // PlayfabManager.Instance.SendLeaderBoard(CoinMananger.Instance.CurrentCoin);

        gameOverPanel.SetActive(true);
        Time.timeScale = 0; // freeze at collum 91 PlayerHealth.cs
    }

    public void GetLeaderBoard() //? nut nhan goi ham nay
    {
        //PlayFabManager.Instance.GetLeaderBoard();
    }

    public void QuitGameAtGameOver() //? thoat khi da chet
    {
        // PlayfabManager.Instance.SendLeaderBoard(CoinMananger.Instance.CurrentCoin); // Gold Leaderboard
        //PlayfabManager.Instance.SavePlayerDataInt();// luu playerData
        Application.Quit();
    }

    public void ContinueGameAtGameOver() //? choi lai scene vua chet
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1; // Unfreeze at collum 91 PlayerHealth.cs
    }

    //public void ReloadCurrentSceneAfterDeath() {
    //    //? load scene hien tai
    //        SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
    //        savingWrapper.Save();
    //        gameOverPanel.SetActive(false);
    //        var currentScene = SceneManager.GetActiveScene();
    //        int currentSceneIndex = currentScene.buildIndex;
    //        SceneManager.LoadSceneAsync(currentSceneIndex);
    //        Time.timeScale = 1; //unfrezze
    //        savingWrapper.Load();
    //        PlayerHealth.Instance.SetCurrentHealth(10);
    //        PlayerHealth.Instance.SetIsPlayerDeath(false);
    //        PlayerHealth.Instance.ResetAnimation();

    //}

    public void ReloadCurrentSceneAfterDeath()
    {
        SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
        savingWrapper.Save();
        gameOverPanel.SetActive(false);
        var currentScene = SceneManager.GetActiveScene();
        int currentSceneIndex = currentScene.buildIndex;

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Load the scene asynchronously
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }

    // Method to handle the sceneLoaded event
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Unsubscribe from the event to prevent multiple calls
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Resume time
        Time.timeScale = 1;

        // Load the saved data
        FindObjectOfType<SavingWrapper>().Load();

        // Reset player health, death status, and animation
        PlayerHealth.Instance.SetCurrentHealth(10);
        PlayerHealth.Instance.SetIsPlayerDeath(false);
        PlayerHealth.Instance.ResetAnimation();
        ActiveWeapon.Instance.ResetAttack();
    }

}
