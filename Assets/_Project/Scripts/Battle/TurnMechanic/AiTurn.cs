using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AiTurn : IState
{
    private readonly TurnStateMachine _turnStateMachine;
    public List<ISubscribeToStateChanged> TickSubscribers { get; }
    public List<ISubscribeToStateChanged> OnEnterSubscribers { get; }
    public List<ISubscribeToStateChanged> OnExitSubscribers { get; }

    public AiTurn(TurnStateMachine turnStateMachine)
    {
        _turnStateMachine = turnStateMachine;
        
        TickSubscribers = new List<ISubscribeToStateChanged>();
        OnEnterSubscribers = new List<ISubscribeToStateChanged>();
        OnExitSubscribers = new List<ISubscribeToStateChanged>();
    }
    
    
    public virtual Task Tick()
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