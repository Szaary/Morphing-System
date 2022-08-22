using System.Collections.Generic;
using UnityEngine;

public interface IModifyStats
{
    List<Modifier> Modifiers { get; set; }

    public bool OnModifyStat(Character character, IOperateStats caller)
    {
        foreach (var modifier in Modifiers)
        {
            foreach (var statistic in character.battleStats.statistics)
            {
                if (modifier.statisticToModify == statistic.baseStatistic)
                {
                    modifier.algorithm.Modify(statistic, modifier, caller);
                }
            }
        }

        return true;
    }
}