using UnityEngine;
using Zenject;

public class HealthMonitor : StatisticMonitor
{
    private ToUiEventsHandler _eventsHandler;

    [Inject]
    public void Construct(ToUiEventsHandler eventsHandler)
    {
        _eventsHandler = eventsHandler;
    }
    
    protected override void OnValueChanged(float modifier, float current)
    {
        if(!isSilent) Debug.Log("Health changed to: "+ current);
        if (current <= 0)
        {
            ChosenStat.OnValueChanged -= OnValueChanged;
            Facade.InvokeDeSpawnedCharacter();
            
            _eventsHandler.HealthChanged?.Invoke(new HealthChanged()
            {
                Modifier = modifier,
                Current = current,
                Position = transform.position
            });
        }
    }
}

