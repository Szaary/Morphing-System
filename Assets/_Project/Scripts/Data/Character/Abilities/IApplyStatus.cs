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
public interface IApplyStatusOverTime : IApplyStatus 
{
    int DurationInTurns { get; set; }
    Character Character { get; set; }
    MonoBehaviour Caller { get; set; }
    
    void SetState(Character character);
    
    new Result OnApplyStatus(Character character, MonoBehaviour caller)
    {
        SetState(character);
        Character = character;
        Caller = caller;
        return Result.Success;
    }

    void TickStatus()
    {
        foreach (var statistic in Character.battleStats.statistics)
        {
            if (StatisticToModify == statistic.baseStatistic)
            {
                Modifier.Modify(statistic, Amounts, Caller);
            }
        }
        
        DurationInTurns--;
        if (DurationInTurns <= 0)
        {
            OnRemoveStatus(Character, Caller);
        }
    }

    new Result OnRemoveStatus(Character character, MonoBehaviour caller)
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

