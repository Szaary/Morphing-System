using UnityEngine;

[CreateAssetMenu(fileName = "ITE_", menuName = "Items/Item")]
public class Item : Passive 
{
    public ItemSlot slot;
    public float weight;
}