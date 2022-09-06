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
    
    public override Result ActivateEffect(List<CharacterFacade> targets, CharacterFacade user)
    {
        foreach (var target in targets)
        {
            var result = OnApplyStatus(target, user);
            ReportError(result);
        }
        
        return Result.Success;
    }

  
    private Result OnApplyStatus(CharacterFacade target, CharacterFacade user)
    {
        if (target == null) return Result.NoTarget;
        if (user == null) return Result.NoUser;
        if (IsAttack())
        {
            target.animatorManager.GetHit();
        }
        return target.Modify(user, Modifiers);
    }
    
    private static void ReportError(Result result)
    {
        if (result == Result.Success) return;
        Debug.Log(typeof(ChangeStatsValue) + " " + result);
        throw new Exception();
    }

}