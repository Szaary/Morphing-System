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
    
    protected override void OnValueChanged(float modifier, float current, Result result)
    {
        Debug.Log("Health changed to: "+ current+ " result: "+ result);
        _eventsHandler.HealthChanged?.Invoke(new HealthChanged()
        {
            Modifier = modifier,
            Current = current,
            Position = transform.position
        });
        
        
        if (result == Result.BelowMin)
        {
            ChosenStat.OnValueChanged -= OnValueChanged;
            Facade.DeSpawnCharacter();
        }
    }

   
}

