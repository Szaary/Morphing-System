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
    
    
    [Header("Statistic that increase base effect. Can be empty.")]
    public BaseStatistic modifier;

    [Header("Set if you add modifier")]
    public float ratio;
}
