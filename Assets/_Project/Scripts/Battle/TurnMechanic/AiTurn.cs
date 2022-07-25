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
    
    
    public override Task Tick()
    {
        foreach (var subscriber in TickSubscribers)
        {
            subscriber.Tick();
        }
        
        bool hasAnyoneActions = false;
        
        foreach (var subscriber in TickSubscribers)
        {
            if (subscriber is IDoActions acting)
            {
                if (acting.CurrentActions > 0)
                {
                    hasAnyoneActions= true;
                }
            }
        }

        if (hasAnyoneActions == false)
        {
            _turnStateMachine.SetState(TurnStateMachine.TurnState.PlayerTurn);
        }

        return Task.CompletedTask;
    }
}