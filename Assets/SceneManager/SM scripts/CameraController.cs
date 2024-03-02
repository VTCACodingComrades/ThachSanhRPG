using Cinemachine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    public void SetPlayerCameraFollow()
    {
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();// tham chieu den camera cua scene hien tai
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform; // gan vi tri player vao camera de follow
    }
}