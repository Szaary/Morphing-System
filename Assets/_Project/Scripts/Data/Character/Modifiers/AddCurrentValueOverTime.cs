using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MOD_", menuName = "Character/Modifiers/AddCurrentValueOverTime")]
public class AddCurrentValueOverTime : AddCurrentValue
{
    public float timeBetweenUpdates = 0.3f;
    public event Action OnValueChanged;
    
    protected override void AddValues(Statistic stats, List<float> modifiers, MonoBehaviour caller)
    {
        caller.StartCoroutine(AddValuesOverTime(stats, modifiers, caller));
    }

    private IEnumerator AddValuesOverTime(Statistic stats, List<float> modifiers, MonoBehaviour caller)
    {
        foreach (var modifier in modifiers)
        {
            AddValue(stats, modifiers, modifier, caller);
            OnValueChanged?.Invoke();
            yield return new WaitForSeconds(timeBetweenUpdates);
        }
    }
}
