using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ITE_", menuName = "Items/Item")]
public class Item : Status 
{
    [Header("To use in skins/variants implementation")]
    [SerializeField] private List<Material> materials;
    
    public ItemSlot slot;
    public float weight;
}