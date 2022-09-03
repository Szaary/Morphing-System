using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TurnBasedInputManager : MonoBehaviour
{
    [SerializeField] private TurnBasedInput turnBasedInput;

    public event Action<List<Active>, List<CharacterFacade>, CharacterFacade, int> InputsPopulated;
    public event Action<Active, List<CharacterFacade>> ActionSelected;
    public event Action ActionActivated;
    public event Action<Result> WrongButtonPressed;


    public List<CharacterFacade> possibleTargets = new();
    public List<Active> possibleActives = new();
    public List<CharacterFacade> chosenTargets = new();


    private int _selectedActiveIndex = -1;
    private Active _selectedActive;


    private TurnStateMachine _stateMachine;
    private CharactersLibrary _library;
    private CharacterFacade _facade;
    private PlayerTurnBasedStrategy _strategy;

    [Inject]
    public void Construct(CharactersLibrary library, TurnStateMachine stateMachine)
    {
        _library = library;
        _stateMachine = stateMachine;

        var result = _library.GetControlledCharacter(out var facade);
        if (result == Result.Success)
        {
            OnControlledCharacterChanged(facade);
        }

        _library.ControlledCharacterChanged += OnControlledCharacterChanged;
        turnBasedInput.ButtonPressed += UseSkill;
    }


    private void UseSkill(int index)
    {
        if (StopInWrongTurn(index) != Result.Success) return;
        // 1. Check possible actions based on list of targets. - show possible skills
        // 2. Select skill - show possible targets
        // 3. Select target based on skills

        if (index == -1)
        {
            _stateMachine.SetState(TurnState.AiTurn);
            return;
        }

        if (_selectedActiveIndex >= 0)
        {
            if (StopWrongTarget(index) != Result.Success) return;

            ActionActivated?.Invoke();

            if (_selectedActive.IsMultiTarget())
            {
                _selectedActive.ActivateEffect(chosenTargets, _facade);
            }
            else
            {
                _selectedActive.ActivateEffect(chosenTargets[0], _facade);
            }
            return;
        }

        if (StopLockedSkill(index) != Result.Success) return;
        SelectSkill(index);
    }

    public void PopulateCurrentState(List<Active> active, List<CharacterFacade> targets, CharacterFacade character,
        int points)
    {
        possibleActives = active;
        possibleTargets = targets;

        InputsPopulated?.Invoke(active, targets, character, points);
    }


    private void SelectSkill(int index)
    {
        _selectedActive = possibleActives.First(x => x.Position == index);
        chosenTargets = TacticsLibrary.SelectTargetsBasedOnActive(_selectedActive, possibleTargets, _facade.Alignment.id);

        if (chosenTargets.Count > 0)
        {
            _selectedActiveIndex = index;
            Debug.Log("Selected skill: " + _selectedActive);
            ActionSelected?.Invoke(_selectedActive, chosenTargets);
        }
        else
        {
            var result = Result.NoSuitableSkillsToUse;
            WrongButtonPressed?.Invoke(result);
        }
    }


    private Result StopInWrongTurn(int i)
    {
        if (_stateMachine.GetCurrentState() != TurnState.PlayerTurn)
        {
            var result = Result.AiTurn;
            WrongButtonPressed?.Invoke(result);
            return result;
        }

        return Result.Success;
    }

    private Result StopWrongTarget(int index)
    {
        if (chosenTargets.Count(x => x.Position == index) == 0)
        {
            var result = Result.NoTarget;
            WrongButtonPressed?.Invoke(result);
            Debug.Log("Chosen target with wrong result: " + index);
            return result;
        }

        Debug.Log("Chosen target : " + index);
        return Result.Success;
    }

    private Result StopLockedSkill(int index)
    {
        if (possibleActives.Count(x => x.Position == index) == 0)
        {
            var result = Result.NoSkillAvailable;
            WrongButtonPressed?.Invoke(result);
            return result;
        }

        return Result.Success;
    }


    public void ResetInputs()
    {
        Debug.Log("Resetting inputs");
        _selectedActiveIndex = -1;
        _selectedActive = null;
        chosenTargets.Clear();
        possibleTargets.Clear();
        possibleActives.Clear();
    }


    private void OnControlledCharacterChanged(CharacterFacade character)
    {
        _facade = character;
        if (character.GetTurnBasedStrategy() is PlayerTurnBasedStrategy playerStrategy)
            _strategy = playerStrategy;
    }

    private void OnDestroy()
    {
        turnBasedInput.ButtonPressed -= UseSkill;
        _library.ControlledCharacterChanged -= OnControlledCharacterChanged;
    }
}