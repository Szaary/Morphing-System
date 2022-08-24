using System;
using UnityEngine;

[RequireComponent(typeof(CharacterFacade))]
public abstract class StatisticMonitor : MonoBehaviour
{
    [SerializeField] protected BaseStatistic statistic;
    [SerializeField] protected bool isSilent = true;
    
    protected CharacterFacade Facade;
    protected Statistic ChosenStat;
    
    protected virtual void Awake()
    {
        Facade = GetComponent<CharacterFacade>();
        Facade.StatisticSet += StatisticsSet;
    }

    private void StatisticsSet(CharacterStatistics statistics)
    {
        if (statistics.GetStatistic(statistic, out ChosenStat) == CharacterStatistics.Result.Success)
        {
            ChosenStat.OnValueChanged -= OnValueChanged;
            ChosenStat.OnValueChanged += OnValueChanged;
        }
    }

    protected abstract void OnValueChanged(float modifier, float current);

    protected void OnValidate()
    {
        if(statistic==null) Debug.LogError("You need to set statistic monitor want to watch.");
    }

    protected virtual void OnDestroy()
    {
        Facade.StatisticSet -= StatisticsSet;
        if(ChosenStat!=null) ChosenStat.OnValueChanged -= OnValueChanged;
    }
}