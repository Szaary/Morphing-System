using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ALI_", menuName = "Statistics/Alignment")]
public class Alignment : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField, Layer] private int factionLayerMask;
    
    public bool IsAlly(Alignment alignment)
    {
        if (alignment == this) return true;
        
        if (ally.Contains(alignment)) return true;
        
        return false;
    }

    public bool IsPlayer
    {
        get
        {
            if (id == 0)
            {
                Debug.Log("It is player character");
                return true;
            }
            else return false;
        }
    }

    public bool IsPlayerAlly
    {
        get
        {
            if (id is 0 or 1) return true;
            else return false;
        }
    }


    public LayerMask FactionLayerMask => factionLayerMask;

    [SerializeField] private List<Alignment> ally;


    private void OnValidate()
    {
        if (IsPlayer)
        {
            if (ally.Count(x => x.IsAlly(this)) > 1)
            {
                Debug.LogError("Need to change const in code to support that ally.");
            }
        }
    }
}

