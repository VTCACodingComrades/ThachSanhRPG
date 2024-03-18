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
    [SerializeField] Button switchButton;

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
        switchButton.onClick.AddListener(SwitchMode);
        ShopChanged();
    }

    private void SwitchMode()
    {
        currentShop.SelectMode(!currentShop.IsBuyingMode());
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
        totalField.text = $"Tong: ${currentShop.TransactionTotal():N2} Vàng";
        totalField.color = currentShop.CanTransact() ? originalTotalTextColor : Color.red;
        TextMeshProUGUI switchText = switchButton.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI confirmText = confirmButton.GetComponentInChildren<TextMeshProUGUI>();
        if (currentShop.IsBuyingMode())
        {
            switchText.text = "Bán";
            confirmText.text = "Mua";
        }
        else
        {
            switchText.text = "Mua";
            confirmText.text = "Mua";
        }
    }

    public void ConfirmTransaction()
    {
        currentShop.ConfirmTransaction();
    }
}
