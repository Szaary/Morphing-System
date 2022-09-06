using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
public class TimeSwitcher : AbstractGameModeSwitcherMono
{
    
    [SerializeField] private TimeManager timeManager;
    [SerializeField] Volume volume;
    [SerializeField] private float timeScale = 0.3f;
    [SerializeField] private float slowDuration = 2;

    protected override void OnGameModeChanged(GameMode newMode)
    {
        timeManager.Slow(timeScale, slowDuration);
        
        DOTween.To(x => volume.weight = x, 0, 1, 0.5f);
         
        StartCoroutine(ReturnToNormal());
    }

    private IEnumerator ReturnToNormal()
    {
        yield return new WaitForSeconds(slowDuration/2);
        DOTween.To(x => volume.weight = x, 1, 0, 0.5f);
    }
}