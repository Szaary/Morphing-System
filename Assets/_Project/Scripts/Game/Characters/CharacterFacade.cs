using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CharacterFacade : MonoBehaviour
{
    public List<ICharacterSystem> CharacterSystems { get; }= new List<ICharacterSystem>();
    
    public MovementManager movement;
    public StatisticsManager stats;
    public AnimatorManager animatorManager;
    
    [Header("Weapons")]
    public MeleeWeaponController meleeWeaponController;
    public RangedWeaponController rangedWeaponController;

    [Header("Turn Based Logic")]
    public TurnController turnController;
    public TurnStatsManager turnStatsManager;
    


    [Header("Realtime Logic")] 
    public RealtimeController realTimeController;
    public RealTimeStatsManager realTimeStatsManager;
    public AiGraphFacade aiGraphFacade;

    #region 2D Logic

    public int GetActionPoints() => stats.character.maxNumberOfActions;
    public BaseSpawnZone.SpawnLocation GetPosition() => stats.character.position;
    public int Position => stats.character.position.index;
    public TurnBasedStrategy GetStrategy() => stats.GetTurnStrategy();
    public ActiveManager ActiveSkillsManager => stats.character.active;
    public Result Modify(CharacterFacade user, List<Modifier> modifiers) => stats.Modify(user, modifiers);
    public Result UnModify(CharacterFacade user, List<Modifier> modifiers) => stats.UnModify(user, modifiers);

    #endregion

    public TurnReferences Turns{ get; private set; }
    public CharactersLibrary Library { get; private set; }
    public CameraManager CameraManager { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public MovementInput MovementInput { get; private set; }

    [HideInInspector] public bool isControlled;
    public GameManager GameManager { get; private set; }
    public TimeManager TimeManager { get; private set; }
    public TurnBasedInputManager BasedInputManager { get; private set; }

 
    
    [Inject]
    public void Construct(Character characterTemplate,
        TurnReferences turns,
        CharactersLibrary library,
        CameraManager cameraManager,
        PlayerInput playerInput,
        MovementInput starterInputs,
        GameManager gameManager,
        TimeManager timeManager,
        TurnBasedInputManager turnBasedInputManager)
    {
        MovementInput = starterInputs;
        PlayerInput = playerInput;
        Turns = turns;
        Library = library;
        CameraManager = cameraManager;
        GameManager = gameManager;
        TimeManager = timeManager;
        BasedInputManager = turnBasedInputManager;
        
        stats.SetCharacter(this, characterTemplate);
        turnStatsManager.Initialize(this);
        turnController.Initialize(this);
        rangedWeaponController.Initialize(this);
        meleeWeaponController.Initialize(this);
        realTimeController.Initialize(this);
        realTimeStatsManager.Initialize(this);
        aiGraphFacade.Initialize(this);

        Library.AddCharacter(this);
    }

    public void SetPosition(BaseSpawnZone.SpawnLocation position)
    {
        stats.character.position = position;
        movement.Initialize(this);
    }

    public void GainControl()
    {
        isControlled = true;
        Library.SetControlledCharacter(this);
    }

    public Result GetStatistic(BaseStatistic baseStatistic, out Statistic outStat) =>
        stats.GetStatistic(baseStatistic, out outStat);

    public void SetBattlePosition(BaseSpawnZone.SpawnLocation playerPosition)
    {
        movement.SetPosition(playerPosition);
    }
    public Alignment Alignment => stats.character.Alignment;
    public string Name => stats.character.data.characterName;
    public void LookAt(Transform position) => transform.LookAt(position);

    public void RemoveCharacter()
    {
        Library.RemoveCharacter(this);
        GetPosition().occupied -= 1;
        Debug.Log("Destroying character: " + name);

        foreach (var system in CharacterSystems)
        {
            system.Disable();
        }
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, Character, CharacterFacade>
    {
    }

    private void OnValidate()
    {
        stats ??= GetComponentInChildren<StatisticsManager>();
        meleeWeaponController ??= GetComponentInChildren<MeleeWeaponController>();
        rangedWeaponController ??= GetComponentInChildren<RangedWeaponController>();
        turnController ??= GetComponentInChildren<TurnController>();
        turnStatsManager ??= GetComponentInChildren<TurnStatsManager>();
        realTimeController ??= GetComponentInChildren<RealtimeController>();
        realTimeStatsManager ??= GetComponentInChildren<RealTimeStatsManager>();
        aiGraphFacade ??= GetComponentInChildren<AiGraphFacade>();
        animatorManager ??= GetComponentInChildren<AnimatorManager>();
    }
    
    
    
    //TODO Temporary here
    public void AnimationWorked()
    {
        turnController.animationWorked = true;
    }

    public void AnimationEnded()
    {
        Debug.Log("Animation ended");
        turnController.animationEnded = true;
    }
    
    
}