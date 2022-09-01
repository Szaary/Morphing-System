using System;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CharacterFacade : MonoBehaviour
{
    public MovementManager movement;
    public StatisticsManager stats;

    [Header("Weapons")] public MeleeWeaponController meleeWeaponController;
    public RangedWeaponController rangedWeaponController;

    [Header("Turn Based Logic")] public TurnController turnController;
    public TurnStatsManager turnStatsManager;
    public TurnReferences Turns;


    [Header("Realtime Logic")] public RealtimeController realTimeController;
    public RealTimeStatsManager realTimeStatsManager;


    #region 2D Logic

    public int GetActionPoints() => stats.character.maxNumberOfActions;
    public BaseSpawnZone.SpawnLocation GetPosition() => stats.character.position;
    public int PositionIndex => stats.character.position.index;
    public TurnBasedStrategy GetTurnBasedStrategy() => stats.character.turnBasedStrategy;
    public RealTimeStrategy GetRealTimeStrategy() => stats.character.realTimeStrategy;
    public ActiveManager ActiveSkillsManager => stats.character.active;
    public Result Modify(CharacterFacade user, List<Modifier> modifiers) => stats.Modify(user, modifiers);
    public Result UnModify(CharacterFacade user, List<Modifier> modifiers) => stats.UnModify(user, modifiers);

    #endregion


    [Header("Camera Logic")] public Transform cameraFpsFollowPoint;
    public Transform cameraFppFollowPoint;


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

        stats.SetCharacter(this, characterTemplate);

        turnStatsManager.SetCharacter(this);
        turnController.Initialize(this);

        rangedWeaponController.Initialize(this);
        meleeWeaponController.Initialize(this);

        realTimeController.Initialize(this);
        realTimeStatsManager.Initialize(this);


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

    public Alignment Alignment => stats.character.alignment;
    public string Name => stats.character.data.characterName;
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

    private void OnValidate()
    {
        stats ??= GetComponentInChildren<StatisticsManager>();
        meleeWeaponController ??= GetComponentInChildren<MeleeWeaponController>();
        rangedWeaponController ??= GetComponentInChildren<RangedWeaponController>();
        turnController ??= GetComponentInChildren<TurnController>();
        turnStatsManager ??= GetComponentInChildren<TurnStatsManager>();
        realTimeController ??= GetComponentInChildren<RealtimeController>();
        realTimeStatsManager ??= GetComponentInChildren<RealTimeStatsManager>();
    }
}