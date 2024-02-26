using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Transform listRoot;
    [SerializeField] RowUI rowPrefab;
    [SerializeField] TextMeshProUGUI totalField;
    [SerializeField] Button confirmButton;

    Shopper shopper = null;
    Shop currentShop = null;
    Color originalTotalTextColor;
    // Start is called before the first frame update
    void Start()
    {
        originalTotalTextColor = totalField.color;
        shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();
        if (shopper == null) return;
        shopper.activeShopChange += ShopChanged;
        confirmButton.onClick.AddListener(ConfirmTransaction);
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
        confirmButton.interactable = currentShop.CanTransact();
        totalField.text = $"Total: ${currentShop.TransactionTotal():N2} Gold";
        totalField.color = currentShop.CanTransact() ? originalTotalTextColor : Color.red;
    }

    public void ConfirmTransaction()
    {
        currentShop.ConfirmTransaction();
    }
}
