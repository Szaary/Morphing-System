using System;
using TMPro;
using UnityEngine;

public class UnitUserInterface : StatisticMonitor
{
    [SerializeField] private TextMeshPro statText;
    [SerializeField] private TextMeshPro characterName;
    [SerializeField] private Transform pivot;

    private GameManager _gameManager;

    public override void Start()
    {
        base.Start();
        characterName.text = Facade.Name;
        SetBar(ChosenStat.CurrentValue, ChosenStat.maxValue);
        _gameManager = Facade.GameManager;
        _gameManager.GameModeChanged += OnGameModeChanged;
    }

    private void OnGameModeChanged(GameMode newMode)
    {
        if (newMode == GameMode.TurnBasedFight)
        {
            pivot.gameObject.SetActive(true);
        }
        else
        {
            pivot.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (pivot.gameObject.activeInHierarchy)
        {
            pivot.transform.rotation =
                Quaternion.LookRotation(pivot.position - Facade.CameraManager.MainCamera.transform.position);
        }
    }

    protected override void OnValueChanged(float modifier, float currentValue, float maxValue, Result result)
    {
        SetBar(currentValue, maxValue);
        if (result == Result.BelowMin) pivot.gameObject.SetActive(false);
        else pivot.gameObject.SetActive(true);
    }

    private void SetBar(float currentValue, float maxValue)
    {
        statText.text = $"{currentValue:0} / {maxValue:0}";
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _gameManager.GameModeChanged -= OnGameModeChanged;
    }
}