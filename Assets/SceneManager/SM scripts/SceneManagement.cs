using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : Singleton<SceneManagement>
{
    //? class nay de hinh thanh va ko bi mat khi loadScene
    //? gia tri bien SceneTransitionName duoc gan khi player cham vao doi tuong ExitEntrance o moi Scene
    //? khi qua scene moi, se so sanh SceneTransitionName == voi gia tri bien nam trong ham Entrance ( start len khi hinh thanh o scene moi)

    public string SceneTransitionName{get; private set; } // {get; private set; }

    public void SetTransitionName(string sceneTransitionName) {
        this.SceneTransitionName = sceneTransitionName;
        Debug.Log("co gan vi tri cho player " + transform.position);
    }

    //todo change scene 18-11-2023
    private Scene currentScene;
    private int currentSceneIndex;
    public int CurrentSceneIndex {get{return currentSceneIndex;}}
    public enum Scenes
    {
        MainMenu, //0
        Scene_00,//1
        Scene_01, //2
        Scene_02, //3
        Scene_03, //4
        Scene_04 //5
    }
    public void ExitGame() {
        Application.Quit();
    }

    public void LoadScene(Scenes scene) //? Load den scene bat ki trong enum
    {
        SceneManager.LoadSceneAsync(scene.ToString());
    }

    public void LoadNewGame() //? khi nhan nut playNow se load den scene_01
    {
        SceneManager.LoadSceneAsync(Scenes.Scene_00.ToString()); // = "Scene_01"
    }

    public void ResumeGame() //? quay tro lai scene dang dung
    {
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }

    public void LoadNextScene() //? Load den scene ke tiep KHONG LIEN QUAN den ontrigger ExitArea.cs
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void BackToMainMenu() 
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = currentScene.buildIndex;
        Debug.Log("scene vua roi khoi = "+ currentSceneIndex);
        
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }

    //todo
}
