using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryFactory
{
    public static Inventory CreateInventory(Action<Item> useItemAction) // Modified method signature
    {
        return new Inventory(useItemAction);
    }
}
