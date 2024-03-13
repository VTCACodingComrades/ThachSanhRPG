using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class MiniMap : Singleton<MiniMap>
{
    [SerializeField] Transform scene03_west;
    [SerializeField] Transform scene02_east;
    [SerializeField] Transform scene02_south;
    [SerializeField] Transform scene01_north;
    [SerializeField] Transform scene01_west;
    [SerializeField] Transform scene00_east;
    [SerializeField] PlayerIcon playerIcon;

    bool isShow = false;
    private Vector3 origianlPosition;
    // Start is called before the first frame update
    void Start()
    {
        origianlPosition = transform.position;
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    MoveObjectToCenter();
    //}

    
    private void LateUpdate()
    {
        if (isShow)
            MoveObjectToCenter();
    }
    public void SetPlayerIconBeginingPosition()
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        string sceneName = SceneManager.GetActiveScene().name;
        string beginingPosition = SceneManagement.Instance.SceneTransitionName;
        //Debug.Log(currentSceneIndex);
        Debug.Log(beginingPosition);
        if (sceneName == "Scene_02" && beginingPosition == "East_Entrance") //Scene 2 East Entrance
        {
            playerIcon.transform.position = scene02_east.position;
        }
        else if (sceneName == "Scene_03" && beginingPosition == "West_Entrance") //Scene 3 West Entrance
        {
            playerIcon.transform.position = scene03_west.position;
        }
        else if (sceneName == "Scene_02" && beginingPosition == "South_Entrance")
        {
            playerIcon.transform.position = scene02_south.position;
        }
        else if (sceneName == "Scene_01" && beginingPosition == "North_Entrance")
        {
            playerIcon.transform.position = scene01_north.position;
        }
        else if (sceneName == "Scene_01" && beginingPosition == "West_Entrance")
        {
            playerIcon.transform.position = scene01_west.position;
        }
        else if (sceneName == "Scene_00" && beginingPosition == "East_Entrance")
        {
            playerIcon.transform.position = scene00_east.position;
        }
    }
    public void ToggleMap()
    {
        if (!isShow)
        {
            isShow = true;
        }
        else
        {
            transform.position = origianlPosition;
            isShow = false;
        }
    }

    void MoveObjectToCenter()
    {
        // Find the center of the screen
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // Convert the center point from screen coordinates to world coordinates
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(center);

        worldCenter.z = 0;

        // Set the GameObject's position to the center of the screen
        transform.position = worldCenter;

    }
}
