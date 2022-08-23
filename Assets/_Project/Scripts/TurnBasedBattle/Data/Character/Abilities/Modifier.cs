using System;
using UnityEngine;

[Serializable]
public struct Modifier
{
    [Header("Statistic you want to modify on target.")]
    public BaseStatistic statisticToModify;

    [Header("Algorithm used to change stat.")]
    public Algorithm algorithm;

    [Header("Base flat change in stat.")] 
    [SerializeField] private float baseAmount;

    [Header("Statistic of ability user that increase base effect. Can be empty.")]
    [SerializeField] private BaseStatistic modifier;

    [Header("Set if you add modifier. Formula is baseAmount * ratio")]
    [SerializeField] private float ratio;

    public float GetAmount(IOperateStats user)
    {
        var amount = baseAmount;
        if (modifier != null)
        {
            foreach (var stat in user.UserStatistics.statistics)
            {
                if (stat.baseStatistic != modifier) continue;
                amount += stat.CurrentValue * ratio;
                break;
            }
        }

        return amount;
    }
}