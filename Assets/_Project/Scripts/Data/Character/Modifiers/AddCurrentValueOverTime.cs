using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MOD_", menuName = "Modifiers/AddCurrentValueOverTime")]
public class AddCurrentValueOverTime : AddCurrentValue
{
    public float timeBetweenUpdates;
    public float timeToEnd;
    public event Action OnValueChanged;
    
    protected override void AddValue(Statistic stats, Modifier modifier, IOperateStats caller)
    {
        caller.Caller.StartCoroutine(AddValuesOverTime(stats, modifier, caller));
    }

    private IEnumerator AddValuesOverTime(Statistic stats, Modifier modifier, IOperateStats caller)
    {
        Debug.LogError("Not done yet");
        base.AddValue(stats, modifier, caller);
        OnValueChanged?.Invoke();
        yield return new WaitForSeconds(timeBetweenUpdates);
    }
}
