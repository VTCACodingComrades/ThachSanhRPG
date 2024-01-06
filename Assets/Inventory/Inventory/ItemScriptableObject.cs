using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject
{
    public Item.ItemType itemType;
    public string itemName;
    public Sprite itemSprite;
    public GameObject pfWeaponType;
    public AnimatorOverrideController animatorOverrideController;
    

    // public CharacterEquipment.EquipSlot equipSlot;

}
