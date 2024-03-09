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

    public void ReloadCurrentSceneAfterDeath() {
        //? load scene hien tai
            gameOverPanel.SetActive(false);
            var currentScene = SceneManager.GetActiveScene();
            int currentSceneIndex = currentScene.buildIndex;
            SceneManager.LoadSceneAsync(currentSceneIndex);
            Time.timeScale = 1; //unfrezze

            PlayerHealth.Instance.SetCurrentHealth(10);
            PlayerHealth.Instance.SetIsPlayerDeath(false);
            PlayerHealth.Instance.ResetAnimation();

    }


}
