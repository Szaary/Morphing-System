using UnityEngine;

[CreateAssetMenu(fileName = "ITE_", menuName = "Items/Item")]
public class Item : Status 
{
    public ItemSlot slot;
    public float weight;
}