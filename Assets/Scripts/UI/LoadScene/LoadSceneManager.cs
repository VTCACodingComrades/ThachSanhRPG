using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;
    void Start()
    {
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        // Load the main scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainMenu");

        // Keep track of the progress of loading
        while (!asyncOperation.isDone)
        {
            // Update the loading bar's value based on the progress
            float progress = Mathf.Clamp01(asyncOperation.progress / .9f);
            loadingBar.value = progress;
            loadingText.text = progress * 100 + "%";
            yield return null;
        }
    }
}
