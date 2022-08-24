using UnityEngine;

public class HealthMonitor : StatisticMonitor
{
    protected override void OnValueChanged(float modifier, float current)
    {
        if(!isSilent) Debug.Log("Health changed to: "+ current);
        if (current <= 0)
        {
            ChosenStat.OnValueChanged -= OnValueChanged;
            Facade.InvokeDeSpawnedCharacter();
        }
    }
}