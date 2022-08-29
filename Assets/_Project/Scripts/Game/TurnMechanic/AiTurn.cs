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

        foreach (var subscriber in TickSubscribers)
        {
            var result = subscriber.Tick();
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

    public override void OnEnter()
    {
        OnEnterBaseImplementation();
    }

    public override void OnExit()
    {
        OnExitBaseImplementation();
    }
}