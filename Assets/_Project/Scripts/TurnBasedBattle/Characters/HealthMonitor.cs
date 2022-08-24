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
        _eventsHandler.HealthChanged?.Invoke(new HealthChanged()
        {
            Modifier = modifier,
            Current = current,
            Position = transform.position
        });
        ChosenStat.OnValueChanged -= OnValueChanged;
        
        if (current <= 0)
        {
            Facade.DeSpawnCharacter();
        }
    }

   
}

