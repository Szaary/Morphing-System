using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
public class TimeSwitcher : AbstractGameModeSwitcherMono
{
    
    [SerializeField] private TimeManager timeManager;
    [SerializeField] Volume volume;
    [SerializeField] private float timeScale = 0.3f;
    [SerializeField] private float slowDuration = 1;

    protected override void OnGameModeChanged(GameMode newMode)
    {
        timeManager.Slow(timeScale, slowDuration);
        volume.weight = 1;
        StartCoroutine(ReturnToNormal());
    }

    private IEnumerator ReturnToNormal()
    {
        yield return new WaitForSeconds(slowDuration);
        volume.weight = 0;
    }
}