using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "CHA_", menuName = "Character/Base")]
public class Character : ScriptableObject
{
    [Header("General")] public CharacterData data;
    public CharacterVFX vfx;
    public CharacterSFX sfx;

    [Header("Statistics")] [SerializeField]
    private CharacterStatistics baseStats;

    public List<Active> activeAbilities;
    public List<PassiveAbility> passiveAbilities;
    public List<PassiveEffect> effects;
    [HideInInspector] public CharacterStatistics battleStats;

    [Header("Items")] public ItemsHolder backpack;
    public ItemsEquipment equipment;

    private IState _playerTurn;


    public bool InitializeStats(MonoBehaviour caller, IState playerTurn)
    {
        _playerTurn = playerTurn;

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

        effects.Clear();

        if (!ApplyPassive(passiveAbilities.ConvertAll(x => (Passive) x), caller)) return false;
        if (!ApplyPassive(equipment.items.ConvertAll(x => (Passive) x), caller)) return false;


        return true;
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
            if (!ApplyPassive(caller, passiveEffect)) return false;
        }

        return true;
    }

    public bool ApplyPassive(MonoBehaviour caller, Passive passiveEffect)
    {
        if (passiveEffect is IApplyStatus passiveModifier)
        {
            if (passiveModifier.OnApplyStatus(this, caller) != IApplyStatus.Result.Success) return false;
        }

        return true;
    }

    public bool ApplyEffect(MonoBehaviour caller, Passive passiveEffect)
    {
        if (passiveEffect is IApplyStatusOverTime passiveModifier)
        {
            if (passiveModifier.OnApplyStatus(this, caller) != IApplyStatus.Result.Success) return false;
        }
        return true;
    }

    public void SetBaseStats(CharacterStatistics statistics)
    {
        baseStats = statistics;
    }

    public IState GetState()
    {
        return _playerTurn;
    }

    private void OnDestroy()
    {
        DestroyBattleStats();
    }
}