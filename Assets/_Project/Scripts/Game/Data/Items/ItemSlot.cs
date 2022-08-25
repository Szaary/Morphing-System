using UnityEngine;

[CreateAssetMenu(fileName = "ITS_", menuName = "Items/ItemSlot")]
public class ItemSlot : ScriptableObject
{
    public string slotName;
    public int maxStack;
    public int currentStack;
    public int maxWearSlots;
}