using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MOD_", menuName = "Modifiers/AddCurrentValue")]
public class AddCurrentValue : Algorithm
{
    public override void Modify(Statistic stats, Modifier modifier, IOperateStats caller)
    {
        AddValue(stats, modifier, caller);
    }

    public override void UnModify(Statistic stats, Modifier modifier, IOperateStats caller)
    {
        RemoveValues(stats, modifier, caller.Caller);
    }


    protected virtual void AddValue(Statistic stats, Modifier modifier, IOperateStats caller)
    {
        var amount = modifier.baseAmount;
        if (modifier.modifier != null)
        {
            foreach (var stat in caller.CharacterStatistics.statistics)
            {
                if (stat.baseStatistic != modifier.modifier) continue;
                amount += stat.CurrentValue*modifier.ratio;
                break;
            }
        }
        
        var result = stats.Add(amount);
        Debug.Log("Added " + modifier+ " to " + stats.baseStatistic.statName + " from " + caller.Caller.name +
                  ". Current value is: " + stats.CurrentValue + " with result: " + result);
    }

    protected virtual void RemoveValues(Statistic stats, Modifier modifier, MonoBehaviour caller)
    {
        var result = stats.Add(-modifier.baseAmount);
        Debug.Log("Removed " + modifier + " from " + stats.baseStatistic.statName + ". Current value is: " +
                  stats.CurrentValue +" with result: " + result);
    }
}