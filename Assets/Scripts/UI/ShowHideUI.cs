using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideUI : MonoBehaviour
{
    [SerializeField] GameObject uiContainer;    
    

    public void Toggle()
    {
        gameObject.SetActive(!uiContainer.activeSelf);
    }
}
