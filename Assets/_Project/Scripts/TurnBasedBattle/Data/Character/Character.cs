using System;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "CHA_", menuName = "Character/Base")]
public class Character : ScriptableObject
{
    public const int MAXActionPoints = 100;
    
    [Header("General")] public CharacterData data;
    public CharacterVFX vfx;
    public CharacterSFX sfx;

    [Header("Basic")] public Alignment alignment;
    [HideInInspector] public int zoneIndex;
    public Strategy strategy;
    
    [Range(1, MAXActionPoints)] public int maxNumberOfActions;
    
    [Header("Statistics")] [SerializeField]
    private List<Statistic> statisticsTemplate;

    [HideInInspector] public List<Statistic> statistics;


    [Header("Abilities")] public ActiveManager active;

    [SerializeField] private List<PassiveAbility> passiveAbilitiesTemplate;
    [SerializeField] private List<PassiveEffect> effectsTemplate;

    [HideInInspector] public List<PassiveAbility> passiveAbilities;
    [HideInInspector] public List<PassiveEffect> effects;

    [Header("Items")] [SerializeField] private ItemsHolder backpackTemplate;
    [SerializeField] private ItemsEquipment equipmentTemplate;
    [HideInInspector] public ItemsHolder backpack;
    [HideInInspector] public ItemsEquipment equipment;


    public void CreateInstances()
    {
        foreach (var stat in statisticsTemplate)
        {
            var clone = stat.Clone();
            statistics.Add(clone);
        }

        Debug.LogWarning("Check if Items are instantiated");
        backpack = backpackTemplate.Clone();
        equipment = equipmentTemplate.Clone();

        foreach (var stat in passiveAbilitiesTemplate)
        {
            var clone = stat.Clone();
            passiveAbilities.Add(clone);
        }

        foreach (var stat in effectsTemplate)
        {
            var clone = stat.Clone();
            effects.Add(clone);
        }
    }

    public void RemoveInstances()
    {
        foreach (var stat in statistics)
        {
            statistics.Remove(stat);
            Destroy(stat);
        }

        backpack = backpackTemplate.Clone();
        equipment = equipmentTemplate.Clone();

        for (var index = passiveAbilities.Count - 1; index >= 0; index--)
        {
            var stat = passiveAbilities[index];
            passiveAbilities.Remove(stat);
            Destroy(stat);
        }

        for (var index = effects.Count - 1; index >= 0; index--)
        {
            var stat = effects[index];
            effects.Remove(stat);
            Destroy(stat);
        }
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (data == null)
        {
            Debug.LogError("No data assigned to character");
        }

        if (alignment == null)
        {
            Debug.LogError("No alignment assigned to character");
        }

        if (vfx == null)
        {
            Debug.LogError("No vfx assigned to character");
        }

        if (sfx == null)
        {
            Debug.LogError("No sfx assigned to character");
        }

        if (statisticsTemplate == null || statisticsTemplate.Count == 0)
        {
            Debug.LogError("No base stats assigned to character");
        }

        if (strategy == null)
        {
            Debug.LogError("No strategy assigned to character");
        }

        if (backpackTemplate == null)
        {
            Debug.LogError("No backpack assigned to character");
        }

        if (zoneIndex is < 0 or > 3)
        {
            Debug.LogError("Wrong zone");
        }

        active.Validate();
    }
#endif
}