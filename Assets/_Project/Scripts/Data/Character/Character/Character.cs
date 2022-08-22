using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//[CreateAssetMenu(fileName = "CHA_", menuName = "Character/Base")]
public class Character : ScriptableObject
{
    [Header("General")] 
    public CharacterData data;
    public CharacterVFX vfx;
    public CharacterSFX sfx;

    
    [Header("Statistics")] 
    public Alignment alignment;
    
    [SerializeField] private CharacterStatistics baseStats;

    public List<Active> activeAbilities;
    public List<PassiveAbility> passiveAbilities;
    public List<PassiveEffect> effects;
    [HideInInspector] public CharacterStatistics battleStats;

    [Header("Items")] public ItemsHolder backpack;
    public ItemsEquipment equipment;

    private BaseState _actionTurn;
    
    private BaseState _playerTurn;
    private BaseState _aiTurn;

    public bool InitializeStats(InitializationArguments arguments)
    {
        SetTurnActions(arguments);
        
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

        if (!ApplyPassive(passiveAbilities.ConvertAll(x => (Passive) x), arguments.caller)) return false;
        if (!ApplyPassive(equipment.items.ConvertAll(x => (Passive) x), arguments.caller)) return false;
        ApplyEffects(arguments.caller);

        return true;
    }

    private void SetTurnActions(InitializationArguments arguments)
    {
        if (alignment.alignment == 0)
        {
            _actionTurn = arguments.playerTurn;
            _playerTurn = arguments.playerTurn;
        }
        else
        {
            _actionTurn = arguments.aiTurn;
            _aiTurn = arguments.aiTurn;
        }
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
            Debug.Log("Applying: " + passiveEffect.name);
            if (passiveModifier.OnApplyStatus(this, caller) != IApplyStatus.Result.Success) return false;
        }

        return true;
    }

    private bool ApplyEffects(MonoBehaviour caller)
    {
        foreach (var effect in effects)
        {
            if (!ApplyEffect(caller, effect)) return false;
        }

        return true;
    }

    public bool ApplyEffect(MonoBehaviour caller, PassiveEffect effect)
    {
        if (effect is IApplyStatusOverTurns)
        {
            var tempEffect = Instantiate(effect);
            if (((IApplyStatusOverTurns) tempEffect).OnApplyStatus(this, caller) !=
                IApplyStatusOverTurns.Result.Success) return false;
        }

        return true;
    }

    public void SetBaseStats(CharacterStatistics statistics)
    {
        baseStats = statistics;
    }

    public BaseState GetState()
    {
        return _actionTurn;
    }

    private void OnDestroy()
    {
        DestroyBattleStats();
    }
    
    public struct InitializationArguments
    {
        public MonoBehaviour caller;
        public BaseState playerTurn;
        public BaseState aiTurn;
    }
}