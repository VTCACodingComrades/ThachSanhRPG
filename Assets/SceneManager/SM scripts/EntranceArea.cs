
using UnityEngine;

public class EntranceArea : MonoBehaviour
{
    // se chay moi khi loadscene
    [SerializeField] private string transitionName;
    //[SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Start() {
        if(transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position = this.transform.position; // gan dc entrance position cho nhan vat

            // TH1 cach di chuyen cam follow player khi chuyen scene theo cach binh thuong tao ra cam va gan vi tri nv vao khi bat dau vo scene moi
            // cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>(); // tao lien ket bien camera
            // cinemachineVirtualCamera.Follow = PlayerController.Instance.transform; // gan vi tri nhan vat cho cam follow theo

            CameraController.Instance.SetPlayerCameraFollow(); // lam cho camera cua scene hien tai follow theo player

            UIFadeChangeScene.Instance.FadeToClear();

            SceneManagement.Instance.SetTransitionName(null); // reset lai gia tri SceneTransitionName = null
        }
        MiniMap.Instance.SetPlayerIconBeginingPosition();
    }
}
