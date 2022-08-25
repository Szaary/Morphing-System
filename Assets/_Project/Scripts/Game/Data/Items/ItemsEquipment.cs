using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IEQ_", menuName = "Items/Equipment")]
public class ItemsEquipment : ItemsCollection
{
    public List<ItemSlot> possibleSlots;
    
    public Reason AddItem(Item item, out Item replacedItem)
    {
        var reason = Reason.Success;
        replacedItem = null;

        foreach (var on in items)
        {
            if (on.slot == item.slot)
            {
                reason = Reason.ItemHasChanged;
                replacedItem = on;
            }
        }

        items.Add(item);
        return reason;
    }
    
    public Reason RemoveItem(Item item)
    {
        if (!items.Contains(item))
        {
            return Reason.ItemIsNotFound;
        }
        
        items.Remove(item);
        return Reason.Success;
    }
}
