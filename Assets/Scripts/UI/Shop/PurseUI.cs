using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PurseUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI banlance;
    Purse shopperPurse;

    private void Start()
    {
        shopperPurse = GameObject.FindGameObjectWithTag("Player").GetComponent<Purse>();
        shopperPurse.onChange += RefeshUI;
        RefeshUI();
    }

    public void RefeshUI()
    {
        //banlance.text = shopperPurse.GetBalance().ToString();
        banlance.text = $"${shopperPurse.GetBalance()}";
    }
}
