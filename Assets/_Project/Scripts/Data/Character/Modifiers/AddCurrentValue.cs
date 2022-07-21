using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MOD_", menuName = "Character/Modifiers/AddCurrentValue")]
public class AddCurrentValue : Modifier
{
    public override void Modify(Statistic stats, List<float> modifiers, MonoBehaviour caller)
    {
        AddValues(stats, modifiers, caller);
    }
    
    public override void UnModify(Statistic stats, List<float> modifiers, MonoBehaviour caller)
    {
        RemoveValues(stats, modifiers, caller);
    }
    
    
    protected virtual void AddValues(Statistic stats, List<float> modifiers, MonoBehaviour caller)
    {
        foreach (var modifier in modifiers)
        {
            AddValue(stats, modifiers, modifier, caller);
        }
    }

    protected virtual void AddValue(Statistic stats, List<float> modifiers, float modifier, MonoBehaviour caller)
    {
        stats.CurrentValue += modifier;
        Debug.Log("Added " + modifiers[0] + " to " + stats.baseStatistic.statName);
    }

    protected virtual void RemoveValues(Statistic stats, List<float> modifiers, MonoBehaviour caller)
    {
        foreach (var modifier in modifiers)
        {
            RemoveValues(stats, modifiers, modifier, caller);
        }
    }

    protected virtual void RemoveValues(Statistic stats, List<float> modifiers, float modifier, MonoBehaviour caller)
    {
        stats.CurrentValue -= modifier;
        Debug.Log("Removed " + modifiers[0] + " to " + stats.baseStatistic.statName);
    }
}