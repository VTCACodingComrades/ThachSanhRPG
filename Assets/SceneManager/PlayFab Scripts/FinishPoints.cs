using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoints : MonoBehaviour
{
    //! gameobect = la cot moc cuoi scene
    //! khi cham se set va get bien "levelInt",unLockLevelInt
    private float timeDelayToLoadScene = 1.5f;
    private void OnTriggerEnter2D(Collider2D other) {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if(playerController) {
            
            UnlockNewLevelPlayfab();

            //SceneManagement.Instance.LoadNextScene();

            StartCoroutine(DelayTimeToladSceneRountine());
        }
    }

    void UnlockNewLevelPlayfab() // tang gia tri unLockLevelInt => hien sang nut chon level
    {
        if(SceneManager.GetActiveScene().buildIndex >= UILevelSelectButton.Instance.UnlockLevelInt)
        {
            UILevelSelectButton.Instance.UnlockLevelInt = SceneManager.GetActiveScene().buildIndex + 1;
            // UILevelSelectButton.Instance.SaveLevelInt("playerLevel", SceneManager.GetActiveScene().buildIndex + 1);
            // UILevelSelectButton.Instance.LoadLevelInt("playerLevel");
        }

        //todo truong hop di lai scene nay, UnlockLevelInt > sceneindex hien tai
        // if(SceneManager.GetActiveScene().buildIndex < UILevelSelectButton.Instance.UnlockLevelInt)
        // {
        //     SceneManagement.Instance.LoadNextScene();
        // }
    }

    IEnumerator DelayTimeToladSceneRountine() {
        yield return new WaitForSeconds(timeDelayToLoadScene);
        SceneManagement.Instance.LoadNextScene();
    }

}
