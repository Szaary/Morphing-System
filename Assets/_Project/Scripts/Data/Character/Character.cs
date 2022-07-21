using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CHA_", menuName = "Character/Character")]
public class Character : ScriptableObject
{
    [SerializeField] private CharacterStatistics baseStats;
    [HideInInspector] public CharacterStatistics currentStats;


    public List<ActiveAbility> activeAbilities;

    public List<PassiveAbility> passiveAbilities;
    public List<PassiveEffect> effects;

    public bool InitializeStats(MonoBehaviour caller)
    {
        try
        {
            currentStats = CreateInstance<CharacterStatistics>();
            currentStats.Initialize(baseStats);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }


        if (!ApplyPassiveAbilities(caller)) return false;
        if (!ApplyPassiveEffects(caller)) return false;

        return true;
    }
    
    private void OnDestroy()
    {
        Destroy(currentStats);
    }

    private bool ApplyPassiveEffects(MonoBehaviour caller)
    {
        foreach (var passiveEffect in effects)
        {
            if (passiveEffect is IModifyStats passiveModifier)
            {
                if (!passiveModifier.OnApplyStatus(this, caller)) return false;
            }
        }

        return true;
    }

    private bool ApplyPassiveAbilities(MonoBehaviour caller)
    {
        foreach (var passiveAbility in passiveAbilities)
        {
            if (passiveAbility is IModifyStats passiveModifier)
            {
                if (!passiveModifier.OnApplyStatus(this, caller)) return false;
            }
        }

        return true;
    }
}