using System;
using UnityEngine;

public class Purse : MonoBehaviour
{
    private float startingBalance = 400f;

    private float balance; // todo balance duoc xet starting collum 118 PlayerData_Logign.cs

    public Action onChange;

    private void Awake()
    {
        //balance = startingBalance; //todo tam command
        //print($"Balance: {balance}");
    }

    public float GetBalance()
    {
        return balance;
    }

    public void UpdateBalance(float amount)
    {
        balance += amount;
        //print($"Balance: {balance}");
        onChange();
    }


    // set diem khi load coin tu playfab
    public float SetBalance(float coin) {
        return this.balance = coin;
    }
}
