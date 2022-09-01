using System;
using UnityEngine;

public abstract class StatisticMonitor : MonoBehaviour
{
    [SerializeField] protected BaseStatistic statistic;
    [SerializeField] protected CharacterFacade Facade;
    [SerializeField] protected bool isSilent = false;

    protected Statistic ChosenStat;
    
    public virtual void Start()
    {
        Facade.GetStatistic(statistic, out ChosenStat);
        
        ChosenStat.OnValueChanged -= OnValueChanged;
        ChosenStat.OnValueChanged += OnValueChanged;
    }
    
    protected abstract void OnValueChanged(float modifier, float currentValue, float maxValue, Result result);
    
    protected void OnDestroy()
    {
        if (ChosenStat != null) ChosenStat.OnValueChanged -= OnValueChanged;
    }

    private void OnValidate()
    {
        if (Facade == null)
        {
            Debug.LogError(name + " facade is not set");
        }
    }
}