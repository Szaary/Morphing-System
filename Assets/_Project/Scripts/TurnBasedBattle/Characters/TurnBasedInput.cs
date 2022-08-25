using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TurnBasedInput : MonoBehaviour
{
    public event Action<Result> WrongWSADPressed;
    public event Action<List<Active>, List<CharacterFacade>, CharacterFacade, int> InputsPopulated;
    public event Action<Active, List<CharacterFacade>> ActionSelected;
    public event Action ActionActivated;
    
    
    private TurnStateMachine _stateMachine;
    [HideInInspector] public PlayerStrategy playerStrategy;

    public Action<Active, List<CharacterFacade>, int> ActivateAction;
    private Active _chosenActive;


    private int _lastAction = -1;
    public List<CharacterFacade> possibleTargets = new();
    public List<Active> possibleActives = new();
    public List<CharacterFacade> chosenTargets = new();


    [Inject]
    public void Construct(TurnStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnUse()
    {
        //if (StopPlayerAction()) return;
    }

    public void OnManageTurn()
    {
        EndTurn();
    }

    private void EndTurn()
    {
        Debug.Log("OnManageTurn");
        _stateMachine.SetState(TurnState.AiTurn);
    }

    public void OnTop()
    {
        if (StopInWrongTurn(0)!= Result.Success) return;
        UseSkill(0);
    }

    public void OnDown()
    {
        if (StopInWrongTurn(1)!= Result.Success) return;
        UseSkill(1);
    }

    public void OnLeft()
    {
        if (StopInWrongTurn(2)!= Result.Success) return;
        UseSkill(2);
    }

    public void OnRight()
    {
        if (StopInWrongTurn(3)!= Result.Success) return;
        UseSkill(3);
    }

    private void UseSkill(int index)
    {
        // 1. Check possible actions based on list of targets. - show possible skills
        // 2. Select skill - show possible targets
        // 3. Select target based on skills

        if (_lastAction >= 0)
        {
            if (StopWrongTarget(index)!= Result.Success) return;
            
            ActionActivated?.Invoke();
            ActivateAction(_chosenActive, chosenTargets, index);
            return;
        }

        if (StopLockedSkill(index)!= Result.Success) return;
        SelectSkill(index);
    }
  

    private void SelectSkill(int index)
    {
        var result = playerStrategy.SelectActive(index, possibleActives, possibleTargets, out _chosenActive,
            out chosenTargets);
        if (result == Result.Success)
        {
            _lastAction = index;
            Debug.Log("Selected skill: " + _chosenActive);
            ActionSelected?.Invoke(_chosenActive, chosenTargets);
        }
        else
        {
            result = Result.NoSuitableSkillsToUse;
            WrongWSADPressed?.Invoke(result);
        }
    }

    public void PopulateCurrentState(List<Active> active, List<CharacterFacade> targets, CharacterFacade character, int points)
    {
        possibleActives = active;
        possibleTargets = targets;
        
        InputsPopulated?.Invoke(active, targets, character, points);
    }
    

    public void ResetInputs()
    {
        Debug.Log("Resetting inputs");
        _lastAction = -1;
        _chosenActive = null;
        chosenTargets.Clear();
        possibleTargets.Clear();
        possibleActives.Clear();
    }

    private Result StopInWrongTurn(int i)
    {
        if (_stateMachine.GetCurrentState() != TurnState.PlayerTurn)
        {
            ResetInputs();
            var result = Result.AiTurn;
            WrongWSADPressed?.Invoke(result);
            return result;
           
        }
        return Result.Success;
    }
    
    private Result StopWrongTarget(int index)
    {
        if (possibleTargets.Count(x => x.Position == index) == 0)
        {
            var result = Result.NoTarget;
            WrongWSADPressed?.Invoke(result);
            return result;
        }
        return Result.Success;
    }

    private Result StopLockedSkill(int index)
    {
        if (possibleActives.Count(x => x.IndexOnBar == index) == 0)
        {
            var result = Result.NoSkillAvailable;
            WrongWSADPressed?.Invoke(result);
            return result;
        }
        return Result.Success;
    }


}