using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterFacade))]
public abstract class StatisticMonitor : MonoBehaviour
{
    [SerializeField] protected BaseStatistic statistic;
    [SerializeField] protected bool isSilent = true;
    
    protected CharacterFacade Facade;
    protected Statistic ChosenStat;

    protected void Awake()
    {
        Facade = GetComponent<CharacterFacade>();
        Facade.StatisticSet += StatisticsSet;
    }

    private void StatisticsSet(Statistic changedStatistic)
    {
        if (statistic == changedStatistic.baseStatistic)
        {
            ChosenStat.OnValueChanged -= OnValueChanged;
            ChosenStat.OnValueChanged += OnValueChanged;
        }
    }

    protected abstract void OnValueChanged(float modifier, float currentValue);


    protected void OnDestroy()
    {
        Facade.StatisticSet -= StatisticsSet;
        if(ChosenStat!=null) ChosenStat.OnValueChanged -= OnValueChanged;
    }
}