using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IHO_", menuName = "Items/Holder")]
public class ItemsHolder : ItemsCollection
{
    public int size;
    public float maxWeight;
    public float currentWeight;
    
    public Reason AddItem(Item item)
    {
        var reason = Reason.Success;

        if (items.Count > size)
        {
            return Reason.InventoryIsFull;
        }
        if (item.weight + currentWeight > maxWeight)
        {
            return Reason.WeightIsFull;
        }

        
        currentWeight += item.weight;
        items.Add(item);
        
        return reason;
    }

    public Reason RemoveItem(Item item)
    {
        if (!items.Contains(item))
        {
            return Reason.ItemIsNotFound;
        }
        currentWeight -= item.weight;
        items.Remove(item);
        return Reason.Success;
    }
}