using UnityEngine;

[RequireComponent(typeof(CharacterFacade))]
public abstract class StatisticMonitor : MonoBehaviour
{
    [SerializeField] protected BaseStatistic statistic;
    [SerializeField] protected bool isSilent = false;

    protected Statistic ChosenStat;
    protected CharacterFacade Facade;
    
    public void Initialize(CharacterFacade characterFacade)
    {
        Facade = characterFacade;
        characterFacade.GetStatistic(statistic, out ChosenStat);
        
        ChosenStat.OnValueChanged -= OnValueChanged;
        ChosenStat.OnValueChanged += OnValueChanged;
    }
    
    protected abstract void OnValueChanged(float modifier, float currentValue, Result result);
    
    protected void OnDestroy()
    {
        if (ChosenStat != null) ChosenStat.OnValueChanged -= OnValueChanged;
    }
}