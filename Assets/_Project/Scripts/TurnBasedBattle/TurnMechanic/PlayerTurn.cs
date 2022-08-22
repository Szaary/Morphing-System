using System;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerTurn : BaseState
{
    private readonly TurnStateMachine _turnStateMachine;

    private bool _hasAnyoneActions;
    public PlayerTurn(TurnStateMachine turnStateMachine) : base()
    {
        _turnStateMachine = turnStateMachine;
    }

    public override async Task Tick()
    {
        _hasAnyoneActions = false;
        
        foreach (var subscriber in TickSubscribers)
        {
            var result = await subscriber.Tick();
            HandleSubscriberResult(result, subscriber);

            if (subscriber is IDoActions {CurrentActions: > 0})
            {
                _hasAnyoneActions = true;
            }
        }

        if (_hasAnyoneActions == false)
        {
            _turnStateMachine.SetState(TurnStateMachine.TurnState.AiTurn);
        }
    }
    
    public override async Task OnEnter()
    {
        await OnEnterBaseImplementation();
    }

    public override async Task OnExit()
    {
        await OnExitBaseImplementation();
    }
}