using System;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "CHA_", menuName = "Character/Base")]
public class Character : ScriptableObject, IOperateStats
{
    [Header("General")] 
    public CharacterData data;
    public CharacterVFX vfx;
    public CharacterSFX sfx;

    
    [Header("Statistics")] 
    public Alignment alignment;
    public Strategy strategy;
    
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
    private MonoBehaviour _caller;
    
    public CharacterStatistics UserStatistics => baseStats != null ? battleStats : baseStats;
    public MonoBehaviour User => _caller;

    public bool InitializeStats(InitializationArguments arguments)
    {
        SetTurnActions(arguments);

        _caller = arguments.caller;
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

        if (!ApplyPassive(passiveAbilities.ConvertAll(x => (Passive) x), this)) return false;
        if (!ApplyPassive(equipment.items.ConvertAll(x => (Passive) x), this)) return false;
        ApplyEffects(this);

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

    private bool ApplyPassive(List<Passive> passives, IOperateStats user)
    {
        foreach (var passiveEffect in passives)
        {
            if (!ApplyPassive(user, passiveEffect)) return false;
        }

        return true;
    }

    public bool ApplyPassive(IOperateStats user, Passive passiveEffect)
    {
        if (passiveEffect is IApplyPersistentStatus passiveModifier)
        {
            Debug.Log("Applying: " + passiveEffect.name);
            if (passiveModifier.OnApplyStatus(this, user) != IApplyPersistentStatus.Result.Success) return false;
        }

        return true;
    }

    private bool ApplyEffects(IOperateStats user)
    {
        foreach (var effect in effects)
        {
            if (!ApplyEffect(user, effect)) return false;
        }

        return true;
    }

    public bool ApplyEffect(IOperateStats user, PassiveEffect effect)
    {
        if (effect is IApplyStatusOverTurns)
        {
            var tempEffect = Instantiate(effect);
            if (((IApplyStatusOverTurns) tempEffect).OnApplyStatus(this, user) !=
                IApplyStatusOverTurns.Result.Success) return false;
        }

        return true;
    }

    public void SetBaseStats(CharacterStatistics statistics)
    {
        baseStats = statistics;
    }

    public BaseState GetState() => _actionTurn;
    public BaseState GetPlayerState() => _playerTurn;
    public BaseState GetAiState() => _aiTurn;
    
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
    
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (data == null)
        {
            Debug.LogError("No data assigned to character");
        }
        if(alignment == null)
        {
            Debug.LogError("No alignment assigned to character");
        }
        if (vfx == null)
        {
            Debug.LogError("No vfx assigned to character");
        }
        if( sfx == null)
        {
            Debug.LogError("No sfx assigned to character");
        }

        if (baseStats == null)
        {
            Debug.LogError("No base stats assigned to character");
        }
   
        if(backpack == null)
        {
            Debug.LogError("No backpack assigned to character");
        }
    }
#endif
    
    
}