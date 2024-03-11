using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenuButton : MonoBehaviour
{
    //! gameobject = la panel mainmenu
    //! nhung nut nhan o day se link voi SceneManagement.cs de lay phuon gthuc chuyen scene

    [SerializeField] private GameObject player;
    [SerializeField] PlayerData_Loggin playerData_Loggin;
    [SerializeField] Button levelsButton;
    private float delayTimeUIButtonPressed = 0.1f;

    private void Awake() {
        //player = GameObject.Find("Player"); //todo find folder Player de khi can playnewGame se xoa this.gameobject
        levelsButton.enabled = false;
    }

    private void Start()
    {
        
    }

    private void Update() {
        player = GameObject.Find("Player");

        if(playerData_Loggin.GetIsRegisteredID_Data) levelsButton.enabled = true;
        else levelsButton .enabled = false;
    }

    public void PlayNewGameButton() 
    {
        //! KHI NHAN NUT PLAY NEW GAME NEU PLAYER DANG TON TAI VA CAN CHOI GAME MOI THI HAY XOA TAI DAY
        //! ly do dang choi quay tro lai mainmenu, doi tuon gplayer van con dang ton tai nhung dang freeze

        if(player) {
            Destroy(PlayerController.Instance.gameObject);
            Debug.Log("CO XOA PLAYER DE VAO NEW GAME ");
        } 
        StartCoroutine(SwitchUIButtonRoutine());
        Time.timeScale = 1; //unfrezze trnh truong hop frezze dong 24 khi backtoMainMenu tu 1 scene nao do
        
        SceneManagement.Instance.LoadNewGame();
    }

    public void QuitGameButton() {
        StartCoroutine(SwitchUIButtonRoutine());
        SceneManagement.Instance.ExitGame();
    }

    public void ResumeGameButton() {
        StartCoroutine(SwitchUIButtonRoutine());
        Time.timeScale = 1; //unfrezze khi truoc do nhan back tu any scene
        SceneManagement.Instance.ResumeGame();
    }

    IEnumerator SwitchUIButtonRoutine() {
        yield return new WaitForSeconds(delayTimeUIButtonPressed);
    }


    //todo 
   
}
