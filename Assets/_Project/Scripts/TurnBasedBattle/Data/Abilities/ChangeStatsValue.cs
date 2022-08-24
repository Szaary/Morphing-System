using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AA_", menuName = "Abilities/Active Ability/ChangeStatsValue")]
public class ChangeStatsValue : Active, IModifyStats
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
        var result = ((IModifyStats) this).OnApplyStatus(target, user);
        if (result != Result.Success)
        {
            Debug.LogError(typeof(ChangeStatsValue) + " result: " + result);
        }
        return actions;
    }
}