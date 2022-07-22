using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemsCollection : ScriptableObject
{
    public enum Reason
    {
        Success,
        InventoryIsFull,
        WeightIsFull,
        ItemIsNotCompatible,
        ItemHasChanged,
        ItemIsNotFound,
    }
    
    public List<Item> items = new List<Item>();
    
    
}
