using UnityEngine;

public class TimeSwitcher : GameModeSwitcherMono
{
    [SerializeField] private TimeManager timeManager;

    [SerializeField] private float timeScale = 0.3f;
    [SerializeField] private float slowDuration = 1;

    protected override void OnGameModeChanged(GameMode newMode)
    {
        timeManager.Slow(timeScale, slowDuration);
    }
}