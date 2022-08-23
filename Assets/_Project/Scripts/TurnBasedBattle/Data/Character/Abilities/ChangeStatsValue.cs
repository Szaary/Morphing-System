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

    public override int ActivateEffect(Character target, IOperateStats user)
    {
        var result = ((IModifyStats) this).OnApplyStatus(target, user);
        if (result != IModifyStats.Result.Success)
        {
            Debug.LogError(typeof(ChangeStatsValue) + " result: " + result);
        }
        return actions;
    }
}