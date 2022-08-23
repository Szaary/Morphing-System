using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MOD_", menuName = "Modifiers/AddCurrentValue")]
public class AddCurrentValue : Algorithm
{
    public override void Modify(Statistic stats, Modifier modifier, IOperateStats user)
    {
        AddValue(stats, modifier, user);
    }

    public override void UnModify(Statistic stats, Modifier modifier, IOperateStats user)
    {
        RemoveValues(stats, modifier, user);
    }


    protected virtual void AddValue(Statistic stats, Modifier modifier, IOperateStats user)
    {
        var result = stats.Add(modifier.GetAmount());
        Debug.Log("Changed " + modifier + " by "+ modifier.GetAmount() + " on stat: " +  stats.baseStatistic.statName + " by " + user.User.name +
                  ". Current value is: " + stats.CurrentValue + " with result: " + result);
    }

    protected virtual void RemoveValues(Statistic stats, Modifier modifier, IOperateStats user)
    {
        var result = stats.Add(-modifier.GetAmount());
        Debug.Log("Removed " + modifier + " from " + stats.baseStatistic.statName + ". Current value is: " +
                  stats.CurrentValue +" with result: " + result);
    }
}