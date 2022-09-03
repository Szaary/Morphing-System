using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AA_", menuName = "Abilities/Active Ability/ChangeStatsValue")]
public class ChangeStatsValue : Active
{
    [Header("Statistics")] [SerializeField]
    private List<Modifier> modifiers;

    [SerializeField]private AnimatorManager.AnimationsType animationType;
    public List<Modifier> Modifiers
    {
        get => modifiers;
        set => modifiers = value;
    }
    
    public override Result ActivateEffect(List<CharacterFacade> targets, CharacterFacade user)
    {
        user.turnController.ApplyCost(cost);
        
        foreach (var target in targets)
        {
            var result = OnApplyStatus(target, user);
            user.LookAt(target.transform);
            ReportError(result);
        }
        return Result.Success;
    }

    public override Result ActivateEffect(CharacterFacade target, CharacterFacade user)
    {
        user.turnController.ApplyCost(cost);
        var result = OnApplyStatus(target, user);
        user.LookAt(target.transform);
        ReportError(result);
        return result;
    }

    private Result OnApplyStatus(CharacterFacade target, CharacterFacade user)
    {
        if (target == null) return Result.NoTarget;
        if (user == null) return Result.NoUser;
        return target.Modify(user, Modifiers);
    }
    
    private static void ReportError(Result result)
    {
        if (result == Result.Success) return;
        Debug.Log(typeof(ChangeStatsValue) + " " + result);
        throw new Exception();
    }

}