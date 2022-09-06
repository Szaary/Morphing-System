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
        OnValidate();
        var result = Facade.GetStatistic(statistic, out ChosenStat);
        if (result != Result.Success)
        {
            Debug.Log(typeof(StatisticMonitor) + " "+ result);
        }
        
        ChosenStat.OnValueChanged -= OnValueChanged;
        ChosenStat.OnValueChanged += OnValueChanged;
    }
    
    protected abstract void OnValueChanged(float modifier, float currentValue, float maxValue, Result result);
    
    protected virtual void OnDestroy()
    {
        if (ChosenStat != null) ChosenStat.OnValueChanged -= OnValueChanged;
    }

    private void OnValidate()
    {
        Facade ??= GetComponent<CharacterFacade>();
        Facade ??= GetComponentInParent<CharacterFacade>();
    }
}