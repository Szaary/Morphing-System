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
    
    List<float> Amounts { get; set; }
    BaseStatistic StatisticToModify { get; set; }
    Modifier Modifier { get; set; }

    Result OnApplyStatus(Character character, MonoBehaviour caller)
    {
        foreach (var statistic in character.battleStats.statistics)
        {
            if (StatisticToModify == statistic.baseStatistic)
            {
                Modifier.Modify(statistic, Amounts, caller);
            }
        }

        return Result.Success;
    }

    Result OnRemoveStatus(Character character, MonoBehaviour caller)
    {
        foreach (var statistic in character.battleStats.statistics)
        {
            if (StatisticToModify == statistic.baseStatistic)
            {
                Modifier.UnModify(statistic, Amounts, caller);
            }
        }

        return Result.Success;
    }
}