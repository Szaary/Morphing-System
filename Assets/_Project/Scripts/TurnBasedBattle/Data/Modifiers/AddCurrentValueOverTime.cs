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
    
    protected override void AddValue(Statistic stats, Modifier modifier, IOperateStats user)
    {
        user.User.StartCoroutine(AddValuesOverTime(stats, modifier, user));
    }

    private IEnumerator AddValuesOverTime(Statistic stats, Modifier modifier, IOperateStats user)
    {
        Debug.LogError("Not done yet");
        base.AddValue(stats, modifier, user);
        OnValueChanged?.Invoke();
        yield return new WaitForSeconds(timeBetweenUpdates);
    }
}
