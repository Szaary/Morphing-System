using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface you need to implement by effects to Over Turn status effects on target.
/// </summary>
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
    Character Target { get; set; }
    IOperateStats User { get; set; }

    void SetState(Character character);

    Result OnApplyStatus(Character character, IOperateStats caller)
    {
        SetState(character);

        Debug.Log("Applying status " + this.GetType().Name + " to " + character.data.characterName);
        Target = character;
        User = caller;
        return Result.Success;
    }

    Result TickStatus()
    {
        Debug.Log("Status effect Tick" + this.GetType().Name);

        foreach (var modifier in Modifiers)
        {
            foreach (var statistic in Target.battleStats.statistics)
            {
                if (modifier.statisticToModify == statistic.baseStatistic)
                {
                    modifier.algorithm.Modify(statistic, modifier, User);
                }
            }
        }

        DurationInTurns--;

        if (DurationInTurns <= 0)
        {
            OnRemoveStatus(Target, User);
            return Result.HasEnded;
        }

        return Result.Success;
    }

    Result OnRemoveStatus(Character character, IOperateStats caller)
    {
        Debug.Log("Removing status " + this.GetType().Name + " from " + character.data.characterName);
        foreach (var modifier in Modifiers)
        {
            foreach (var statistic in Target.battleStats.statistics)
            {
                if (modifier.statisticToModify == statistic.baseStatistic)
                {
                    modifier.algorithm.UnModify(statistic, modifier, User);
                }
            }
        }

        return Result.Success;
    }
}