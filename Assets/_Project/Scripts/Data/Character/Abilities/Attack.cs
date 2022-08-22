using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AA_", menuName = "Abilities/Active Ability/Attack")]
public class Attack : Active, IModifyStats
{
    [Header("Statistics")]
    [SerializeField] private List<Modifier> modifiers;
   
    [Header("Amount of action points need to use action")]
    public int actions;


    public List<Modifier> Modifiers
    {
        get => modifiers;
        set => modifiers = value;
    }
}

