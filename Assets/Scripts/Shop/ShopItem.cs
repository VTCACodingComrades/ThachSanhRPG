using PlayFab.EconomyModels;
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
}
