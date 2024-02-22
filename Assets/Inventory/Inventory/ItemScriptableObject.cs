using PlayFab.EconomyModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject, ISerializationCallbackReceiver
{
    [Tooltip("Auto-generated UUID for saving/loading. Clear this field if you want to generate a new one.")]
    public string itemID = null;
    public Item.ItemType itemType;
    public string itemName;
    public Sprite itemSprite;
    public GameObject pfSword;
    public AnimatorOverrideController animatorOverrideController;
    public int damage;

    // STATE
    static Dictionary<string, ItemScriptableObject> itemLookupCache;


    public string GetDisplayName()
    {
        return itemName;
    }

    // public CharacterEquipment.EquipSlot equipSlot;
    public static ItemScriptableObject GetFromID(string itemID)
    {
        if (itemLookupCache == null)
        {
            itemLookupCache = new Dictionary<string, ItemScriptableObject>();
            var itemList = Resources.LoadAll<ItemScriptableObject>("");
            foreach (var item in itemList)
            {
                if (itemLookupCache.ContainsKey(item.itemID))
                {
                    Debug.LogError(string.Format("Looks like there's a duplicate GameDevTV.UI.InventorySystem ID for objects: {0} and {1}", itemLookupCache[item.itemID], item));
                    continue;
                }

                itemLookupCache[item.itemID] = item;
            }
        }

        if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
        return itemLookupCache[itemID];
    }

    public void OnBeforeSerialize()
    {
        // Generate and save a new UUID if this is blank.
        if (string.IsNullOrWhiteSpace(itemID))
        {
            itemID = System.Guid.NewGuid().ToString();
        }
    }

    public void OnAfterDeserialize()
    {
        //throw new System.NotImplementedException();
    }
}
