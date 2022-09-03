using System;
using TMPro;
using UnityEngine;

public class UnitUserInterface : StatisticMonitor
{
    [SerializeField] private TextMeshPro statText;
    [SerializeField] private TextMeshPro characterName;
    [SerializeField] private Transform pivot;

    public override void Start()
    {
        base.Start();
        characterName.text = Facade.Name;
        SetBar(ChosenStat.CurrentValue, ChosenStat.maxValue);
    }

    private void Update()
    {
        transform.rotation =
            Quaternion.LookRotation(pivot.position - Facade.CameraManager.MainCamera.transform.position);
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