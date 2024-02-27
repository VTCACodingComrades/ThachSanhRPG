using PlayFab.EconomyModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
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

    [Range(0, 100)]
    [SerializeField] float sellingPercentage = 80f;

    Dictionary<ItemScriptableObject, int> transaction = new Dictionary<ItemScriptableObject, int>();

    Dictionary<ItemScriptableObject, int> stock = new Dictionary<ItemScriptableObject, int>();

    bool isBuyingMode = true;

    Shopper currentShopper = null;

    public event Action onChange;

    private void Awake()
    {
        foreach (var stockItem in stockConfig)
        {
            stock[stockItem.item] = stockItem.initialStock;
        }
    }

    public void SetShop()
    {
        GameObject.Find("Player").GetComponent<Shopper>().SetActiveShop(this);
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
            float price = GetPrice(config);
            int quantityInTransaction = 0;
            transaction.TryGetValue(config.item, out quantityInTransaction);
            int availability = GetAvailability(config.item);
            yield return new ShopItem(config.item, availability, price, quantityInTransaction);
        }
    }

    private float GetPrice(StockItemConfig config)
    {
        if (isBuyingMode)
        {
            return config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
        }

        return config.item.GetPrice() * (sellingPercentage / 100);
    }

    public void AddToTransaction(ItemScriptableObject item, int quantity)
    {
        if (!transaction.ContainsKey(item))
        {
            transaction[item] = 0;
        }

        int availbility = GetAvailability(item);

        if (transaction[item] + quantity > availbility)
        {
            transaction[item] = availbility;
        }
        else
        {
            transaction[item] += quantity;
        }

        if (onChange != null)
        {
            onChange();
        }
    }

    public void SelectFilter(ItemCategory category) { }
    public ItemCategory GetFilter() { return ItemCategory.None; }
    
    public bool CanTransact() 
    {
        if (HasEmtyTransaction()) return false;
        if (!HasSufficientFund()) return false;
        return true; 
    }

    public void ConfirmTransaction() {
        Inventory shopperInventory = currentShopper.GetComponent<PlayerController>().GetPlayerInventory();
        Purse shopperPurse = currentShopper.GetComponent<Purse>();
        if (shopperInventory == null || shopperPurse == null) return;

        // Transfer to or from the inventory
        foreach (ShopItem shopItem in GetAllItems())
        {
            ItemScriptableObject item = shopItem.GetItem();
            int quantity = shopItem.GetQuantityInTransaction();
            float price = shopItem.GetPrice();
            for (int i = 0; i < quantity; i++)
            {
                if (isBuyingMode)
                {
                    BuyItem(shopperInventory, shopperPurse, item, price);
                }
                else
                {
                    SellItem(shopperInventory, shopperPurse, item, price);
                }
            }    
                
        }
        // Removal from transaction
        // Debting or Crediting of funds
        if (onChange != null)
        {
            onChange();
        }
    }

    private void SellItem(Inventory shopperInventory, Purse shopperPurse, ItemScriptableObject item, float price)
    {
        AddToTransaction(item, -1);
        shopperInventory.RemoveItem(new Item { itemScriptableObject = item, amount = 1 });
        stock[item]++;
        shopperPurse.UpdateBalance(price);
    }

    private void BuyItem(Inventory shopperInventory, Purse shopperPurse, ItemScriptableObject item, float price)
    {
        if (shopperPurse.GetBalance() < price) return;
        shopperInventory.AddItem(new Item { itemScriptableObject = item, amount = 1 });
        AddToTransaction(item, -1);
        stock[item]--;
        shopperPurse.UpdateBalance(-price);
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
    

    public bool HasSufficientFund()
    {
        if (!isBuyingMode) return true;
        Purse shopperPurse = currentShopper.GetComponent<Purse>();
        return shopperPurse.GetBalance() >= TransactionTotal();
    }

    public void SelectMode(bool isBuying)
    {
        isBuyingMode = isBuying;
        ResetTransaction();
        if (onChange != null)
        {
            onChange();
        }
    }

    private void ResetTransaction()
    {
        transaction.Clear();
    }

    public bool IsBuyingMode()
    {
        return isBuyingMode;
    }

    private bool HasEmtyTransaction()
    {
        return transaction.Count == 0;
    }

    private int GetAvailability(ItemScriptableObject item)
    {
        if (isBuyingMode)
        {
            return stock[item];
        }
        else
        {
            return CountItemsInInventory(item);
        }

    }

    private int CountItemsInInventory(ItemScriptableObject item)
    {
        Inventory shopperInventory = currentShopper.GetComponent<PlayerController>().GetPlayerInventory();
        int itemAmount = 0;
        foreach(Item inventoryItem in shopperInventory.GetItemList())
        {
            if (inventoryItem.itemScriptableObject == item)
            {
                itemAmount = inventoryItem.amount;
            }
        }
        return itemAmount;
    }
}
