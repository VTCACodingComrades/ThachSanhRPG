using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
