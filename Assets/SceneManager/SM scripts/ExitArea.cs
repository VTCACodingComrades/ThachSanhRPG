using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitArea : MonoBehaviour
{
    // se chay moi khi co va cham
    [SerializeField] private string sceneToLoad; // scene 2
    [SerializeField] private string sceneTransitionName; // west_En
    private float waitToLoad = 1f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            //SceneManager.LoadScene(sceneToLoad); // load ngay tuc thi ko can hieu ung
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);// west_En
            
            UIFadeChangeScene.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
    }
    IEnumerator LoadSceneRoutine()
    {
        while (waitToLoad >=0)
        {
            waitToLoad -=Time.deltaTime;
            yield return null;
        }
        // yield return new WaitForSeconds(waitToLoad); // delay
        SceneManager.LoadScene(sceneToLoad); // qua scene 2
    }
}
