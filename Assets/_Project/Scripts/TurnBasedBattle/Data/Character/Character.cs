using System;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "CHA_", menuName = "Character/Base")]
public class Character : ScriptableObject, IOperateStats
{
    public event Action<Alignment, Alignment> AlignmentChanged;
    
    [Header("General")] 
    public CharacterData data;
    public CharacterVFX vfx;
    public CharacterSFX sfx;

    
    [Header("Statistics")] 
    [SerializeField] private Alignment alignment;
    public Strategy strategy;
    
    [SerializeField] private CharacterStatistics baseStats;

    public ActiveManager active; 
    
    public List<PassiveAbility> passiveAbilities;
    public List<PassiveEffect> effects;
    
    [Header("Items")] public ItemsHolder backpack;
    public ItemsEquipment equipment;


    public CharacterStatistics UserStatistics { get; private set; }
    public MonoBehaviour User { get; private set; }

    public Alignment Alignment
    {
        get => alignment;
        set
        {
            AlignmentChanged?.Invoke(alignment, value);
            alignment = value;
        }
    }


    private BaseState _actionTurn;
    private BaseState _playerTurn;
    private BaseState _aiTurn;


    public CharacterStatistics InitializeStats(InitializationArguments arguments)
    {
        SetTurnActions(arguments);

        User = arguments.caller;
        try
        {
            UserStatistics = CreateInstance<CharacterStatistics>();
            UserStatistics.Initialize(baseStats);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

        if (!ApplyPassive(passiveAbilities.ConvertAll(x => (Passive) x), this)) HandlePassivesAddingError();
        if (!ApplyPassive(equipment.items.ConvertAll(x => (Passive) x), this)) HandlePassivesAddingError();
        ApplyEffects(this);

        return UserStatistics;
    }

    private void HandlePassivesAddingError()
    {
        Debug.LogError("Something went wrong with adding skills");
    }

    private void SetTurnActions(InitializationArguments arguments)
    {
        if (Alignment.Id == 0)
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
        if (UserStatistics != null)
        {
            UserStatistics.Destroy();
            Destroy(UserStatistics);
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
        if(Alignment == null)
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

        active.Validate();
        
    }
#endif
}