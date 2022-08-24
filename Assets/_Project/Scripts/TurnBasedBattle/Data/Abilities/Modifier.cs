using System;
using UnityEngine;

[Serializable]
public class Modifier
{
    private const int MaxBaseAmount = 100;
    private const int MaxRatioAmount = 100;

    [Header("Statistic you want to modify on target.")]
    public BaseStatistic statisticToModify;

    [Header("Algorithm used to change stat.")]
    public Algorithm algorithm;

    [Header("Base flat change in stat.")] [Range(0, MaxBaseAmount)] [SerializeField]
    private float baseAmount;

    [Header("Statistic of ability user that increase base effect. Can be empty.")]
    [SerializeField] private BaseStatistic modifierStatistic;

    [Header("Set if you add modifier. Formula is modulo(baseAmount + modifier ratio * stat value)")]
    [Range(0, MaxRatioAmount)]
    [SerializeField]
    private float ratio;

    public float GetAmount(CharacterFacade user)
    {
        if (modifierStatistic != null)
        {
            user.GetStatistic(modifierStatistic, out var userStat);
            var amount = baseAmount + ratio * userStat.CurrentValue;
            return amount;
        }
        return baseAmount;
    }
}