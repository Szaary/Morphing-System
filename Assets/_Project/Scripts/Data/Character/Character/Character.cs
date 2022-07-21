using System;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "CHA_", menuName = "Character/Base")]
public class Character : ScriptableObject
{
    [Header("General")]
    public CharacterData data;
    public CharacterVFX vfx;
    public CharacterSFX sfx;
    
    [Header("Statistics")]
    [SerializeField] private CharacterStatistics baseStats;
    public List<ActiveAbility> activeAbilities;
    public List<PassiveAbility> passiveAbilities;
    public List<PassiveEffect> effects;
    [HideInInspector] public CharacterStatistics battleStats;

    
    public bool InitializeStats(MonoBehaviour caller)
    {
        try
        {
            battleStats = CreateInstance<CharacterStatistics>();
            battleStats.Initialize(baseStats);
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

    public void SetBaseStats(CharacterStatistics statistics)
    {
        baseStats = statistics;
    }
    
    public void DestroyBattleStats()
    {
        if (battleStats != null)
        {
            baseStats.Destroy();
            Destroy(battleStats);
        }
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
    
    private void OnDestroy()
    {
        DestroyBattleStats();
    }
}