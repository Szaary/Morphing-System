using System;
using System.Collections;
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

    [Header("Items")]
    public ItemsHolder backpack;
    public ItemsEquipment equipment;
    
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

        if (!ApplyPassive(passiveAbilities.ConvertAll(x => (Passive)x), caller)) return false;
        if (!ApplyPassive(effects.ConvertAll(x => (Passive)x), caller)) return false;
        if (!ApplyPassive(equipment.items.ConvertAll(x => (Passive)x), caller)) return false;
        
        
        return true;
    }

    public void SetBaseStats(CharacterStatistics statistics)
    {
        baseStats = statistics;
    }
    
    private void DestroyBattleStats()
    {
        if (battleStats != null)
        {
            battleStats.Destroy();
            Destroy(battleStats);
        }
    }
    
    private bool ApplyPassive(List<Passive> passives, MonoBehaviour caller)
    {
        foreach (var passiveEffect in passives)
        {
            if (passiveEffect is IModifyStats passiveModifier)
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