using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface you need to implement by persistent effects to apply status effects on target.
/// </summary>
public interface IApplyPersistentStatus : IModifyStats
{
    Result OnRemoveStatus(Character target, IOperateStats user)
    {
        foreach (var modifier in Modifiers)
        {
            foreach (var statistic in target.battleStats.statistics)
            {
                if (modifier.statisticToModify == statistic.baseStatistic)
                {
                    modifier.algorithm.UnModify(statistic, modifier, user);
                }
            }
        }

        return Result.Success;
    }
}