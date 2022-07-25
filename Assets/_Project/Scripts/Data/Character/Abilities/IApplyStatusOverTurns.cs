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
    
    List<float> Amounts { get; set; }
    BaseStatistic StatisticToModify { get; set; }
    Modifier Modifier { get; set; }
    
    int DurationInTurns { get; set; }
    Character Character { get; set; }
    MonoBehaviour Caller { get; set; }

    void SetState(Character character);

    Result OnApplyStatus(Character character, MonoBehaviour caller)
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
            return Result.HasEnded;
        }

        return Result.Success;
    }

    Result OnRemoveStatus(Character character, MonoBehaviour caller)
    {
        Debug.Log("Removing status " + this.GetType().Name + " from " + character.data.characterName);
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