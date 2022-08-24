using UnityEngine;

[RequireComponent(typeof(CharacterFacade))]
public abstract class StatisticMonitor : MonoBehaviour
{
    [SerializeField] protected BaseStatistic statistic;
    [SerializeField] protected bool isSilent = true;

    protected Statistic ChosenStat;
    protected CharacterFacade Facade;
    
    public void Initialize(CharacterFacade characterFacade)
    {
        Facade = characterFacade;
        characterFacade.GetStatistic(statistic, out ChosenStat);
        
        ChosenStat.OnValueChanged -= OnValueChanged;
        ChosenStat.OnValueChanged += OnValueChanged;
    }
    
    protected abstract void OnValueChanged(float modifier, float currentValue);
    
    protected void OnDestroy()
    {
        if (ChosenStat != null) ChosenStat.OnValueChanged -= OnValueChanged;
    }
}