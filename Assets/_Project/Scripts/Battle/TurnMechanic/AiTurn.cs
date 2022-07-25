using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AiTurn : BaseState
{
    private readonly TurnStateMachine _turnStateMachine;


    public AiTurn(TurnStateMachine turnStateMachine)
    {
        _turnStateMachine = turnStateMachine;
    }


    public override async Task Tick()
    {
        bool hasAnyoneActions = false;

        foreach (var subscriber in TickSubscribers)
        {
            var result = await subscriber.Tick();
            HandleSubscriberResult(result, subscriber);

            if (subscriber is IDoActions {CurrentActions: > 0})
            {
                hasAnyoneActions = true;
            }
        }


        if (hasAnyoneActions == false)
        {
            _turnStateMachine.SetState(TurnStateMachine.TurnState.PlayerTurn);
        }
    }
}