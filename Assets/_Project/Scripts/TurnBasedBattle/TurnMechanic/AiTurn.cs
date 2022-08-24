using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AiTurn : BaseState
{
    public AiTurn(TurnStateMachine turnStateMachine) : base(turnStateMachine)
    {
    }

    public override async Task Tick()
    {
        bool hasAnyoneActions = false;

        foreach (var subscriber in TickSubscribers)
        {
            var result = await subscriber.Tick();
            HandleSubscriberResult(result, subscriber);

            if (subscriber is IDoActions {ActionPoints: > 0})
            {
                hasAnyoneActions = true;
            }
        }

        if (hasAnyoneActions == false)
        {
            _stateMachine.SetState(TurnState.PlayerTurn);
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