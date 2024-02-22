using PlayFab.EconomyModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{ 

    public event Action onChange;
    //public List<ItemScriptableObject> items;

    // Stock Config
    // Item: 
    // InventoryItem
    // Initial Stock
    // buyingDiscount
    [SerializeField]
    StockItemConfig[] stockConfig;

    [System.Serializable]
    class StockItemConfig
    {
        public ItemScriptableObject item;
        public int initialStock;
        [Range(0, 100)]
        public float buyingDiscountPercentage;
    }

    Dictionary<ItemScriptableObject, int> transaction = new Dictionary<ItemScriptableObject, int>();

    public IEnumerable<ShopItem> GetFilteredItems() {

        foreach (StockItemConfig config in stockConfig)
        {
            float price = config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
            int quantityInTransaction = 0;
            transaction.TryGetValue(config.item, out quantityInTransaction);
            yield return new ShopItem(config.item, config.initialStock, price, quantityInTransaction);
        }
    }
    public void AddToTransaction(ItemScriptableObject item, int quantity) {
        print($"Added To Transaction: {item.GetDisplayName()} x {quantity}");
        if (!transaction.ContainsKey(item))
        {
            transaction[item] = 0;
        }

        transaction[item] += quantity;

        if (transaction[item] <= 0)
        {
            transaction.Remove(item);
        }

        if (onChange != null)
        {
            onChange();
        }
    }
    public void SelectFilter(ItemCategory category) { }
    public ItemCategory GetFilter() { return ItemCategory.None; }
    public void SelectMode(bool isBuying) { }
    public bool IsBuyingMode() { return true; }
    public bool CanTransact() { return true; }
    public void ConfirmTransaction() { }
    public float TransactionTotal() { return 0; }
    

    public void SetShop()
    {
        GameObject.Find("Player").GetComponent<Shopper>().SetActiveShop(this);
    }
}
