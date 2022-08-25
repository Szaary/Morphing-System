using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterFacade : MonoBehaviour
{
    public TurnController turnController;
    public CharacterManager manager;
    public TurnReferences Turns;
    
    [HideInInspector] public TurnBasedInput playerInput;
    public CharactersLibrary Library;

    [Inject]
    public void Construct(Character characterTemplate, TurnReferences turns, 
        TurnBasedInput input,
        CharactersLibrary library)
    {
        Turns = turns;
        playerInput = input;
        Library = library;
        
        manager.SetCharacter(characterTemplate);
        turnController.Initialize(this);
    }


    public BaseState GetPlayTurn(bool workOnOppositeTurn) => Turns.GetPlayTurn(this, workOnOppositeTurn);
    
    public Result GetStatistic(BaseStatistic baseStatistic, out Statistic outStat) =>
        manager.GetStatistic(baseStatistic, out outStat);
    public Result Modify(CharacterFacade user, List<Modifier> modifiers) => manager.Modify(user, modifiers);
    public Result UnModify(CharacterFacade user, List<Modifier> modifiers) => manager.UnModify(user, modifiers);
    public Alignment Alignment => manager.character.alignment;
    public ActiveManager ActiveSkillsManager => manager.character.active;
    public Strategy GetStrategy() => manager.character.strategy;
    public int Position => manager.character.position;
    public void SetZoneIndex(int index) => manager.character.position = index;

    public string Name => manager.character.data.characterName;
    public int GetActionPoints() => manager.character.maxNumberOfActions;
    
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