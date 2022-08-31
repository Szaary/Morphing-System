using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AA_", menuName = "Abilities/Active Ability/ChangeStatsValue")]
public class ChangeStatsValue : Active
{
    [Header("Statistics")] [SerializeField]
    private List<Modifier> modifiers;

    public List<Modifier> Modifiers
    {
        get => modifiers;
        set => modifiers = value;
    }

    public override int ActivateEffect(CharacterFacade target, CharacterFacade user)
    {
        var result = OnApplyStatus(target, user);
        if (result == Result.Success)
        {
            return actions;
        }
        else
        {
            return 0;
        }
    }

    Result OnApplyStatus(CharacterFacade target, CharacterFacade user)
    {
        if (target == null) return Result.NoTarget;
        if (user == null) return Result.NoUser;
        
        return target.Modify(user, Modifiers);
    }
}