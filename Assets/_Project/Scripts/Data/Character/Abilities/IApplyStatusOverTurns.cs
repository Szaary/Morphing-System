using System.Collections.Generic;
using UnityEngine;

public interface IApplyStatusOverTurns
{
    public enum Result
    {
        Success,
        Resistant,
        HasEnded
    }

    List<Modifier> Modifiers { get; set; }

    int DurationInTurns { get; set; }
    Character Character { get; set; }
    IOperateStats Caller { get; set; }

    void SetState(Character character);

    Result OnApplyStatus(Character character, IOperateStats caller)
    {
        SetState(character);

        Debug.Log("Applying status " + this.GetType().Name + " to " + character.data.characterName);
        Character = character;
        Caller = caller;
        return Result.Success;
    }

    Result TickStatus()
    {
        Debug.Log("Status effect Tick" + this.GetType().Name);

        foreach (var modifier in Modifiers)
        {
            foreach (var statistic in Character.battleStats.statistics)
            {
                if (modifier.statisticToModify == statistic.baseStatistic)
                {
                    modifier.algorithm.Modify(statistic, modifier, Caller);
                }
            }
        }

        DurationInTurns--;

        if (DurationInTurns <= 0)
        {
            OnRemoveStatus(Character, Caller);
            return Result.HasEnded;
        }

        return Result.Success;
    }

    Result OnRemoveStatus(Character character, IOperateStats caller)
    {
        Debug.Log("Removing status " + this.GetType().Name + " from " + character.data.characterName);
        foreach (var modifier in Modifiers)
        {
            foreach (var statistic in Character.battleStats.statistics)
            {
                if (modifier.statisticToModify == statistic.baseStatistic)
                {
                    modifier.algorithm.UnModify(statistic, modifier, Caller);
                }
            }
        }

        return Result.Success;
    }
}