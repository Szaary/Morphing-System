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
    [Header("Do not set anything here, zone will change in playmode")]
    public int zoneIndex;
    public Strategy strategy;
    
    [Range(1, MAXActionPoints)] public int maxNumberOfActions;
    
    [Header("Statistics")] [SerializeField]
    private List<Statistic> statisticsTemplate;

    [Header("Do not set anything here, stats will change in playmode")]
    public List<Statistic> statistics;


    [Header("Abilities")] public ActiveManager active;

    public List<Passive> templatePassives;
    public List<Effect> templateEffects;
    
    private readonly List<Passive> _passives = new();
    private readonly List<Effect> _effects = new();

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
    }

    public void RemoveInstances()
    {
        for (var index = statistics.Count - 1; index >= 0; index--)
        {
            var stat = statistics[index];
            statistics.Remove(stat);
            Destroy(stat);
        }

        backpack = backpackTemplate.Clone();
        equipment = equipmentTemplate.Clone();

        for (var index = _passives.Count - 1; index >= 0; index--)
        {
            var stat = _passives[index];
            _passives.Remove(stat);
            Destroy(stat);
        }

        for (var index = _effects.Count - 1; index >= 0; index--)
        {
            var stat = _effects[index];
            _effects.Remove(stat);
            Destroy(stat);
        }
    }

    public void AddPassive(Passive passive) => _passives.Add(passive);
    public void AddEffect(Effect status) => _effects.Add(status);
    public void RemovePassive(Passive passive) => _passives.Remove(passive);
    public void RemoveEffect(Effect status) => _effects.Remove(status);
    public List<Effect> Effect => _effects;
    public List<Passive> Passives => _passives;
    
    
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