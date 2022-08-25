using System;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CharacterFacade : MonoBehaviour
{
    public CharacterModeSwitcher switcher;
    public TurnController turnController;
    public CharacterManager manager;
    public TurnReferences Turns;
    public Transform cameraFpsFollowPoint;
    public Transform cameraFppFollowPoint;
    public FirstPersonController fpsController;
    public Gun characterGun;
    
    [HideInInspector] public TurnBasedInput turnBasedInput;
    public CharactersLibrary Library;
    [HideInInspector] public CameraManager cameraManager;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public FpsInput starterInputs;

    [HideInInspector] public bool isControlled;
    [HideInInspector] public GameManager gameManager;

    [Inject]
    public void Construct(Character characterTemplate, TurnReferences turns, 
        TurnBasedInput turnBasedInput, CharactersLibrary library, CameraManager cameraManager,
        PlayerInput playerInput, FpsInput starterInputs, GameManager gameManager)
    {
        this.starterInputs = starterInputs;
        this.playerInput = playerInput;
        Turns = turns;
        this.turnBasedInput = turnBasedInput;
        Library = library;
        this.cameraManager = cameraManager;
        this.gameManager = gameManager;
        
        manager.SetCharacter(characterTemplate);
        
        turnController.Initialize(this);
        fpsController.Initialize(this);
        switcher.Initialize(this);
        
        Library.AddCharacter(this);
        characterGun.Initialize(this);
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

    public void LookAt(Transform position)
    {
        transform.LookAt(position);
    }
    
    #region 2D Logic
    public int GetActionPoints() => manager.character.maxNumberOfActions;
    public void SetZoneIndex(int index) => manager.character.position = index;
    public int Position => manager.character.position;
    public Strategy GetStrategy() => manager.character.strategy;
    public ActiveManager ActiveSkillsManager => manager.character.active;
    public Result Modify(CharacterFacade user, List<Modifier> modifiers) => manager.Modify(user, modifiers);
    public Result UnModify(CharacterFacade user, List<Modifier> modifiers) => manager.UnModify(user, modifiers);
    #endregion
    
    
    public void DeSpawnCharacter()
    {
        Library.RemoveCharacter(this);
        Debug.Log("Destroying character: " + name);
        Destroy(gameObject);
    }

    public class Factory : PlaceholderFactory<Character, CharacterFacade>
    {
    }
}