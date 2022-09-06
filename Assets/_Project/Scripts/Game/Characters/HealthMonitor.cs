using UnityEngine;
using Zenject;

public class HealthMonitor : StatisticMonitor
{
    private ToUiEventsHandler _eventsHandler;
    private bool isDead;
    [Inject]
    public void Construct(ToUiEventsHandler eventsHandler)
    {
        _eventsHandler = eventsHandler;
    }
    
    protected override void OnValueChanged(float modifier, float current, float maxValue, Result result)
    {
        Debug.Log("Health changed to: "+ current+ " result: "+ result);
        _eventsHandler.HealthChanged?.Invoke(new HealthChanged()
        {
            Modifier = modifier,
            Current = current,
            Position = Facade.transform.position
        });
        
        
        if (result == Result.BelowMin)
        {
            if (isDead) return;
            isDead = true;
            //ChosenStat.OnValueChanged -= OnValueChanged;
            Facade.animatorManager.Death();
            Facade.RemoveCharacter();
        }
        else
        {
            isDead = false;
        }
    }
}

