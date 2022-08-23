using UnityEngine;

public class StatisticMonitor : MonoBehaviour
{
    [SerializeField] protected BaseStatistic statistic;
    [SerializeField] protected CharacterFacade facade;
    [SerializeField] protected bool isSilent = true;
    
    protected Statistic chosenStat;
    protected void Awake()
    {
        facade.StatisticSet += StatisticsSet;
    }

    private void StatisticsSet(CharacterStatistics statistics)
    {
        if (statistics.GetStatistic(statistic, out chosenStat) == CharacterStatistics.Result.Success)
        {
            chosenStat.OnValueChanged -= OnValueChanged;
            chosenStat.OnValueChanged += OnValueChanged;
        }
    }

    protected virtual void OnValueChanged(float currentValue)
    {
    }

    protected void OnDestroy()
    {
        facade.StatisticSet -= StatisticsSet;
        if(chosenStat!=null) chosenStat.OnValueChanged -= OnValueChanged;
    }
}