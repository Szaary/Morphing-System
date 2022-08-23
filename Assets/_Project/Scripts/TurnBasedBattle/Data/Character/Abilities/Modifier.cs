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
    public float baseAmount;

    [Header("Statistic of ability user that increase base effect. Can be empty.")]
    public BaseStatistic modifier;

    [Header("Set if you add modifier. Formula is baseAmount * ratio")]
    public float ratio;

    public float GetAmount()
    {
        if (modifier != null)
        {
            var amount = baseAmount * ratio;
            return amount;
        }
        return baseAmount;
    }
}