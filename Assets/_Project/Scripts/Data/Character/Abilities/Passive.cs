using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive : ScriptableObject, IModifyStats
{
    [Header("VFX")]
    public string skillName;
    public Sprite icon;
    
    
    
    [Header("Statistics")]
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
