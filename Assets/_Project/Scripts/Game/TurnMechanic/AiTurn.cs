using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AiTurn : BaseState
{
    public AiTurn(TurnStateMachine turnStateMachine) : base(turnStateMachine)
    {
    }

    public override void Tick()
    {
        bool hasAnyoneActions = false;

        for (var index = TickSubscribers.Count - 1; index >= 0; index--)
        {
            var subscriber = TickSubscribers[index];
            var result = subscriber.Tick();
            HandleSubscriberResult(result, subscriber);

            if (subscriber is IDoActions { ActionPoints: > 0 })
            {
                hasAnyoneActions = true;
            }
        }

        if (hasAnyoneActions == false)
        {
            _stateMachine.SetState(TurnState.PlayerTurn);
        }
    }

    public override void OnEnter()
    {
        OnEnterBaseImplementation();
    }

    public override void OnExit()
    {
        OnExitBaseImplementation();
    }
}