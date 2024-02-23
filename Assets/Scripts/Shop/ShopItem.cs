using PlayFab.EconomyModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    ItemScriptableObject item;
    int availability;
    float price;
    int quantityInTransaction;

    public ShopItem(ItemScriptableObject item, int availability, float price, int quantityInTransaction)
    {
        this.item = item;
        this.availability = availability;
        this.price = price;
        this.quantityInTransaction = quantityInTransaction;
    }

    public Sprite GetIcon()
    {
        return item.itemSprite;
    }

    public int GetAvailability()
    {
        return availability;
    }

    public string GetName()
    {
        return item.GetDisplayName();
    }

    public float GetPrice()
    {
        return price;
    }

    public ItemScriptableObject GetItem()
    {
        return item;
    }

    public int GetQuantityInTransaction()
    {
        return quantityInTransaction;
    }
}
