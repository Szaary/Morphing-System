using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Basic interface to modify stats by effects/abilities
/// </summary>
public interface IModifyStats
{
    public enum Result
    {
        Success,
        Resistant,
    }
    List<Modifier> Modifiers { get; set; }

    Result OnApplyStatus(Character target, IOperateStats user)
    {
        foreach (var modifier in Modifiers)
        {
            foreach (var statistic in target.battleStats.statistics)
            {
                if (modifier.statisticToModify  == statistic.baseStatistic)
                {
                    modifier.algorithm.Modify(statistic, modifier, user);
                }
            }
        }
        

        return Result.Success;
    }

}