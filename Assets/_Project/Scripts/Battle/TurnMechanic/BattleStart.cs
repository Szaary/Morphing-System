
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleStart : IState
{
    private readonly TurnStateMachine _turnStateMachine;
    public List<ISubscribeToStateChanged> TickSubscribers { get; }
    public List<ISubscribeToStateChanged> OnEnterSubscribers { get; }
    public List<ISubscribeToStateChanged> OnExitSubscribers { get; }

    public BattleStart(TurnStateMachine turnStateMachine)
    {
        _turnStateMachine = turnStateMachine;
        TickSubscribers = new List<ISubscribeToStateChanged>();
        OnEnterSubscribers = new List<ISubscribeToStateChanged>();
        OnExitSubscribers = new List<ISubscribeToStateChanged>();
    }
    
    public virtual Task OnEnter()
    {
        Debug.Log("Entered state: " + GetType().Name);
        foreach (var subscriber in OnEnterSubscribers)
        {
            subscriber.OnEnter();
        }
        _turnStateMachine.SetState(TurnStateMachine.TurnState.PlayerTurn);
        return Task.CompletedTask;
    }
}
