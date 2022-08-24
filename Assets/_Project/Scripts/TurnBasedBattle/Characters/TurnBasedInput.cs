using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Zenject;

public class TurnBasedInput : MonoBehaviour
{
    private TurnStateMachine _stateMachine;
    public PlayerStrategy playerStrategy;

    public Action<Active, List<CharacterFacade>, int> ActivateAction;
    
    private int _lastAction=-1;
    
    public List<CharacterFacade> possibleTargets = new();
    public List<Active> possibleActives = new();

    public List<CharacterFacade> chosenTargets = new();
    private Active _chosenActive;
    
    [Inject]
    public void Construct(TurnStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnUse()
    {
        if (StopPlayerAction()) return;
        Debug.Log("OnUse");
    }

    public void OnManageTurn()
    {
        Debug.Log("OnManageTurn");
        _stateMachine.SetState(TurnState.AiTurn);
    }

    public void OnTop()
    {
        if (StopPlayerAction()) return;
        UseSkill(0);
    }

    public void OnDown()
    {
        if (StopPlayerAction()) return;
        UseSkill(1);
    }

    public void OnLeft()
    {
        if (StopPlayerAction()) return;
        UseSkill(2);
    }

    public void OnRight()
    {
        if (StopPlayerAction()) return;
        UseSkill(3);
    }

    private void UseSkill(int index)
    {
        // 1. Check possible actions based on list of targets. - show possible skills
        // 2. Select skill - show possible targets
        // 3. Select target based on skills

        for (var i = possibleTargets.Count - 1; i >= 0; i--)
        {
            var target = possibleTargets[i];
            if (target == null)
            {
                possibleTargets.Remove(target);
            }
        }
        if (possibleTargets.Count(x => x.GetZoneIndex() == index) == 0)
        {
            _lastAction = -1;
            return;
        }


        if (_lastAction >= 0)
        {
            Debug.Log("Activated skill: "+ index);
            ActivateAction(_chosenActive, chosenTargets, index);
            ResetInputs();
            return;
        }
        
        if (possibleActives.Count(x => x.IndexOnBar == index) == 0)
        {
            Debug.Log("Skill with index : "+ index + " is locked");
            return;
        }
        SelectSkill(index);
    }

    private void SelectSkill(int index)
    {
        if (playerStrategy.SelectActive(index, possibleActives, possibleTargets, out _chosenActive, out chosenTargets) ==
            PlayerStrategy.Result.Success)
        {
            _lastAction = index;
            Debug.Log("Selected skill: " + _chosenActive);
            return;
        }
        ResetInputs();
    }

 
    private bool StopPlayerAction()
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
        _lastAction = -1;
        _chosenActive = null;
    }
}