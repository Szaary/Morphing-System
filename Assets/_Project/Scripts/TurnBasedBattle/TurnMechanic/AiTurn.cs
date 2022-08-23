using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AiTurn : BaseState
{
    private readonly TurnStateMachine _turnStateMachine;
    private readonly CharacterFactory _characterFactory;


    public AiTurn(TurnStateMachine turnStateMachine,
        CharacterFactory characterFactory)
    {
        _turnStateMachine = turnStateMachine;
        _characterFactory = characterFactory;
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
            _turnStateMachine.SetState(TurnStateMachine.TurnState.PlayerTurn);
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