using PlayFab.EconomyModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Shopper currentShopper = null;

    public event Action onChange; 

    [System.Serializable]
    class StockItemConfig
    {
        public ItemScriptableObject item;
        public int initialStock;
        [Range(0, 100)]
        public float buyingDiscountPercentage;
    }

    [SerializeField]
    StockItemConfig[] stockConfig;

    Dictionary<ItemScriptableObject, int> transaction = new Dictionary<ItemScriptableObject, int>();

    Dictionary<ItemScriptableObject, int> stock = new Dictionary<ItemScriptableObject, int>();


    private void Awake()
    {
        foreach (var stockItem in stockConfig)
        {
            stock[stockItem.item] = stockItem.initialStock;
        }
    }

    public void SetShopper(Shopper shopper)
    {
        currentShopper = shopper;
    }

    public IEnumerable<ShopItem> GetFilteredItems() 
    {
        return GetAllItems();
    }

    public IEnumerable<ShopItem> GetAllItems()
    {
        foreach (StockItemConfig config in stockConfig)
        {
            float price = config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
            int quantityInTransaction = 0;         
            transaction.TryGetValue(config.item, out quantityInTransaction);
            int quantityInStock = stock[config.item];          
            yield return new ShopItem(config.item, quantityInStock, price, quantityInTransaction);
        }
    }
    public void AddToTransaction(ItemScriptableObject item, int quantity) {
        //print($"Added To Transaction: {item.GetDisplayName()} x {quantity}");
        if (!transaction.ContainsKey(item))
        {
            transaction[item] = 0;
        }

        if (transaction[item] + quantity > stock[item])
        {
            transaction[item] = stock[item];
        }
        else
        {
            transaction[item] += quantity;
        }    
        

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
    public void ConfirmTransaction() {
        Inventory shopperInventory = currentShopper.GetComponent<PlayerController>().GetPlayerInventory();
        Purse shopperPurse = currentShopper.GetComponent<Purse>();
        if (shopperInventory == null || shopperPurse == null) return;

        // Transfer to or from the inventory
        //var transactionSnapshot = new Dictionary<ItemScriptableObject, int>(transaction);
        foreach (ShopItem shopItem in GetAllItems())
        {
            ItemScriptableObject item = shopItem.GetItem();
            int quantity = shopItem.GetQuantityInTransaction();
            float price = shopItem.GetPrice();
            for (int i = 0; i < quantity; i++)
            {
                if (shopperPurse.GetBalance() < price) break;
                shopperInventory.AddItem(new Item { itemScriptableObject = item, amount = 1 });
                AddToTransaction(item, -1);
                stock[item]--;
                shopperPurse.UpdateBalance(-price);
            }
        }
        // Removal from transaction
        // Debting or Crediting of funds
        if (onChange != null)
        {
            onChange();
        }
    }
    public float TransactionTotal() 
    {
        float total = 0;
        foreach (ShopItem item in GetAllItems())
        {
            total += item.GetPrice() * item.GetQuantityInTransaction();
        }
        return total;
    }
    

    public void SetShop()
    {
        GameObject.Find("Player").GetComponent<Shopper>().SetActiveShop(this);
    }
}
