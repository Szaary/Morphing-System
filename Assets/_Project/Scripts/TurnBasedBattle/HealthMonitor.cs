using UnityEngine;

public class HealthMonitor : StatisticMonitor
{
    protected override void OnValueChanged(float currentValue)
    {
        base.OnValueChanged(currentValue);
        Debug.Log("Health changed to: "+ currentValue);
        if (currentValue <= 0)
        {
            chosenStat.OnValueChanged -= OnValueChanged;
            facade.InvokeDeSpawnedCharacter();
        }
    }
}