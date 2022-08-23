using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AA_", menuName = "Abilities/Active Ability/Attack")]
public class Attack : Active, IModifyStats
{
    [Header("Statistics")]
    [SerializeField] private List<Modifier> modifiers;
   



    public List<Modifier> Modifiers
    {
        get => modifiers;
        set => modifiers = value;
    }
    
    public override int ActivateEffect(Character target, IOperateStats user)
    {
        var result = ((IModifyStats) this).OnApplyStatus(target, user);
        Debug.Log("Attack result: "+ result);
        return actions;
    }
}

