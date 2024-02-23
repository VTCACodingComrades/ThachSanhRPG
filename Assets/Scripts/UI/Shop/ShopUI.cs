using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Transform listRoot;
    [SerializeField] RowUI rowPrefab;

    Shopper shopper = null;
    Shop currentShop = null;

    // Start is called before the first frame update
    void Start()
    {
        shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();
        if (shopper == null) return;

        shopper.activeShopChange += ShopChanged;
        ShopChanged();
    }

    private void ShopChanged()
    {
        if (currentShop != null)
        {
            currentShop.onChange -= RefreshUI;
        }
        currentShop = shopper.GetActiveShop();
        currentShop.onChange += RefreshUI;
        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (Transform child in listRoot)
        {
            Destroy(child.gameObject);
        }

        foreach (ShopItem item in currentShop.GetFilteredItems())
        {
            RowUI row = Instantiate<RowUI>(rowPrefab, listRoot);
            row.Setup(currentShop, item);
        }
    }

    public void ConfirmTransaction()
    {
        currentShop.ConfirmTransaction();
    }
}
