using System;
using System.Collections.Generic;
using UnityEngine;

public interface IApplyStatus
{
    public enum Result
    {
        Success,
        Resistant,
    }
    
    List<Modifier> Modifiers { get; set; }

    Result OnApplyStatus(Character character, IOperateStats caller)
    {
        foreach (var modifier in Modifiers)
        {
            foreach (var statistic in character.battleStats.statistics)
            {
                if (modifier.statisticToModify  == statistic.baseStatistic)
                {
                    modifier.algorithm.Modify(statistic, modifier, caller);
                }
            }
        }
        

        return Result.Success;
    }

    Result OnRemoveStatus(Character character, IOperateStats caller)
    {
        foreach (var modifier in Modifiers)
        {
            foreach (var statistic in character.battleStats.statistics)
            {
                if (modifier.statisticToModify  == statistic.baseStatistic)
                {
                    modifier.algorithm.UnModify(statistic, modifier, caller);
                }
            }

        }

        return Result.Success;
    }
}