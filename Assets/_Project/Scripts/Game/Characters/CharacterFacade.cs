using System;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CharacterFacade : MonoBehaviour
{
    public CharacterModeSwitcher switcher;
    public StatisticsManager manager;
    public RelativeController relativeController;
    
    [Header("Weapons")]
    public MeleeWeaponController meleeWeaponController;
    public RangedWeaponController rangedWeaponController;
    
    [Header("Turn Based Logic")]
    public TurnController turnController;
    public TurnStatsManager turnStatsManager;
    public TurnReferences Turns;
    
    
    [Header("Realtime Logic")]
    public RealtimeController realTimeController;
    public RealTimeStatsManager realTimeStatsManager;


    #region 2D Logic
    public int GetActionPoints() => manager.character.maxNumberOfActions;
    public BaseSpawnZone.SpawnLocation GetPosition() => manager.character.position;
    public int PositionIndex => manager.character.position.index;
    public TurnBasedStrategy GetTurnBasedStrategy() => manager.character.turnBasedStrategy;
    public RealTimeStrategy GetRealTimeStrategy() => manager.character.realTimeStrategy;
    public ActiveManager ActiveSkillsManager => manager.character.active;
    public Result Modify(CharacterFacade user, List<Modifier> modifiers) => manager.Modify(user, modifiers);
    public Result UnModify(CharacterFacade user, List<Modifier> modifiers) => manager.UnModify(user, modifiers);
    #endregion

    
    [Header("Fps Logic")]
    public Transform cameraFpsFollowPoint;
    public Transform cameraFppFollowPoint;
    public FirstPersonController fpsController;
    
    
    [HideInInspector] public TurnBasedInput turnBasedInput;
    public CharactersLibrary Library;
    [HideInInspector] public CameraManager cameraManager;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public MovementInput movementInput;

    [HideInInspector] public bool isControlled;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public TimeManager timeManager;

    [Inject]
    public void Construct(Character characterTemplate,
        TurnReferences turns,
        TurnBasedInput turnBasedInput,
        CharactersLibrary library,
        CameraManager cameraManager,
        PlayerInput playerInput,
        MovementInput starterInputs,
        GameManager gameManager,
        TimeManager timeManager)
    {
        this.movementInput = starterInputs;
        this.playerInput = playerInput;
        Turns = turns;
        this.turnBasedInput = turnBasedInput;
        Library = library;
        this.cameraManager = cameraManager;
        this.gameManager = gameManager;
        this.timeManager = timeManager;
        
        manager.SetCharacter(this, characterTemplate);
        
        turnStatsManager.SetCharacter(this);
        turnController.Initialize(this);
        relativeController.Initialize(this);
        
        rangedWeaponController.Initialize(this);
        meleeWeaponController.Initialize(this);
        fpsController.Initialize(this);
        realTimeController.Initialize(this);
        realTimeStatsManager.Initialize(this);
        
        
        Library.AddCharacter(this);
    }
    public void SetPosition(BaseSpawnZone.SpawnLocation position)
    {
        manager.character.position = position;
        switcher.Initialize(this);
    }

    public void GainControl()
    {
        isControlled = true;
        Library.SetControlledCharacter(this);
    }
    
    public Result GetStatistic(BaseStatistic baseStatistic, out Statistic outStat) =>
        manager.GetStatistic(baseStatistic, out outStat);
    public Alignment Alignment => manager.character.alignment;
    public string Name => manager.character.data.characterName;
    public void LookAt(Transform position) => transform.LookAt(position);

    public void DeSpawnCharacter()
    {
        Library.RemoveCharacter(this);
        GetPosition().occupied -= 1;
        Debug.Log("Destroying character: " + name);
        Destroy(gameObject);
    }
    
    public class Factory : PlaceholderFactory<UnityEngine.Object, Character, CharacterFacade>
    {
    }

}