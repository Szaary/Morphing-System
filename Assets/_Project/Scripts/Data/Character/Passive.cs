using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive : ScriptableObject, IModifyStats
{
    [SerializeField] private Statistic statisticToModify;
    [SerializeField] private Modifier modifier;
    [SerializeField] private List<float> amounts;

    public List<float> Amounts
    {
        get => amounts;
        set => amounts = value;
    }
    public Statistic StatisticToModify
    {
        get => statisticToModify;
        set => statisticToModify = value;
    }

    public Modifier Modifier
    {
        get => modifier;
        set => modifier = value;
    }
}
