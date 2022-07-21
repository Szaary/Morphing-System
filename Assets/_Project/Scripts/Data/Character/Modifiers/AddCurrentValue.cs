using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MOD_", menuName = "Character/Modifiers/AddCurrentValue")]
public class AddCurrentValue : Modifier
{
    public override void Modify(Statistic stats, List<float> modifiers)
    {
        foreach (var modifier in modifiers)
        {
            stats.CurrentValue += modifier;
            Debug.Log("Added " + modifiers[0] + " to " + stats.baseStatistic.statName);
        }
        
    }

    public override void UnModify(Statistic stats, List<float> modifiers)
    {
        foreach (var modifier in modifiers)
        {
            stats.CurrentValue -= modifier;
            Debug.Log("Removed " + modifiers[0] + " to " + stats.baseStatistic.statName);
        }
    }
}