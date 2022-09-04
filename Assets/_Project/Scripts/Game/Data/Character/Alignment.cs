using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ALI_", menuName = "Statistics/Alignment")]
public class Alignment : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private LayerMask factionLayerMask;
    
    public bool IsAlly(Alignment alignment)
    {
        if (alignment == this) return true;
        else return false;
    }

    public bool IsPlayer
    {
        get
        {
            if (id == 0) return true;
            else return false;
        }
    }


    public LayerMask FactionLayerMask => factionLayerMask;
    
    
}