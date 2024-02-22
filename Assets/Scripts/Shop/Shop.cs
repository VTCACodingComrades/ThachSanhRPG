using PlayFab.EconomyModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{ 

    public event Action onChange;
    public List<ItemScriptableObject> items;

    public IEnumerable<ShopItem> GetFilteredItems() {

        foreach (var item in items)
        {
            yield return new ShopItem(item, 10, 10.0f, 0);
        }      
    }
    public void SelectFilter(ItemCategory category) { }
    public ItemCategory GetFilter() { return ItemCategory.None; }
    public void SelectMode(bool isBuying) { }
    public bool IsBuyingMode() { return true; }
    public bool CanTransact() { return true; }
    public void ConfirmTransaction() { }
    public float TransactionTotal() { return 0; }
    public void AddToTransaction(InventoryItem item, int quantity) { }

    public void SetShop()
    {
        GameObject.Find("Player").GetComponent<Shopper>().SetActiveShop(this);
    }
}
