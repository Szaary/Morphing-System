using TMPro;
using UnityEngine;

public class UnitUserInterface : StatisticMonitor
{
    [SerializeField] private TextMeshPro statText;
    [SerializeField] private TextMeshPro characterName;
    
    
    public override void Start()
    {
        base.Start();
        characterName.text = Facade.Name;
        SetBar(ChosenStat.CurrentValue, ChosenStat.maxValue);
    }

    protected override void OnValueChanged(float modifier, float currentValue, float maxValue, Result result)
    {
        SetBar(currentValue, maxValue);
    }

    private void SetBar(float currentValue, float maxValue)
    {
        statText.text = $"{currentValue:0} / {maxValue:0}";
    }
}