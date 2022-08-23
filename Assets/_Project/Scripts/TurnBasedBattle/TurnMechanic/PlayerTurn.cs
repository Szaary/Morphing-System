using System;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerTurn : BaseState
{
    private readonly TurnStateMachine _turnStateMachine;
    private readonly TargetSelector _targetSelector;
    
    private bool _hasAnyoneActions;
    private readonly CharactersLibrary _charactersLibrary;

    public PlayerTurn(TurnStateMachine turnStateMachine, 
        TargetSelector targetSelector,
        CharactersLibrary charactersLibrary)
    {
        _turnStateMachine = turnStateMachine;
        _targetSelector = targetSelector;
        _charactersLibrary = charactersLibrary;
    }

    public override async Task Tick()
    {
        _hasAnyoneActions = false;
        
        foreach (var subscriber in TickSubscribers)
        {
            var result = await subscriber.Tick();
            HandleSubscriberResult(result, subscriber);

            if (subscriber is IDoActions {ActionPoints: > 0})
            {
                _hasAnyoneActions = true;
            }
        }

        if (_charactersLibrary.PlayerCharacters == 0)
        {
            _turnStateMachine.SetState(TurnStateMachine.TurnState.Defeat);
            return;
        }
        
        if (_charactersLibrary.AiCharacters == 0)
        {
            _turnStateMachine.SetState(TurnStateMachine.TurnState.Victory);
            return;
        }
        
        if (_hasAnyoneActions == false)
        {
            _turnStateMachine.SetState(TurnStateMachine.TurnState.AiTurn);
            return;
        }
    }
    
    public override async Task OnEnter()
    {
        await OnEnterBaseImplementation();
    }

    public override async Task OnExit()
    {
        _targetSelector.DeSelectTarget();
        await OnExitBaseImplementation();
    }
}