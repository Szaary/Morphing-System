using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ALI_", menuName = "Statistics/Alignment")]
public class Alignment : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private LayerMask factionLayerMask;
    
    public int ID => id;

    public LayerMask FactionLayerMask => factionLayerMask;
}