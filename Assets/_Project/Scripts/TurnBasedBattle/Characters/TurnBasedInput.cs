using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TurnBasedInput : MonoBehaviour
{
    private TurnStateMachine _stateMachine;
    public PlayerStrategy playerStrategy;

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
        Debug.Log("OnManageTurn");
        _stateMachine.SetState(TurnState.AiTurn);
    }

    public void OnTop()
    {
        if (StopInWrongTurn(0)) return;
        UseSkill(0);
    }

    public void OnDown()
    {
        if (StopInWrongTurn(1)) return;
        UseSkill(1);
    }

    public void OnLeft()
    {
        if (StopInWrongTurn(2)) return;
        UseSkill(2);
    }

    public void OnRight()
    {
        if (StopInWrongTurn(3)) return;
        UseSkill(3);
    }

    private void UseSkill(int index)
    {
        // 1. Check possible actions based on list of targets. - show possible skills
        // 2. Select skill - show possible targets
        // 3. Select target based on skills

        if (_lastAction >= 0)
        {
            if (StopWrongTarget(index)) return;
            
            ActivateAction(_chosenActive, chosenTargets, index);
            return;
        }

        if (StopLockedSkill(index)) return;
        SelectSkill(index);
    }

    private bool StopWrongTarget(int index)
    {
        if (possibleTargets.Count(x => x.GetZoneIndex() == index) == 0)
        {
            Debug.Log("Target with index : " + index + " do not exist");
            return true;
        }
        return false;
    }

    private bool StopLockedSkill(int index)
    {
        if (possibleActives.Count(x => x.IndexOnBar == index) == 0)
        {
            Debug.Log("Skill with index : " + index + " is locked");
            return true;
        }

        return false;
    }

    private void SelectSkill(int index)
    {
        if (playerStrategy.SelectActive(index, possibleActives, possibleTargets, out _chosenActive,
                out chosenTargets) ==
            Result.Success)
        {
            _lastAction = index;
            Debug.Log("Selected skill: " + _chosenActive);
        }
    }


    private bool StopInWrongTurn(int i)
    {
        if (_stateMachine.GetCurrentState() != TurnState.PlayerTurn)
        {
            ResetInputs();
            return true;
        }

        return false;
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
}