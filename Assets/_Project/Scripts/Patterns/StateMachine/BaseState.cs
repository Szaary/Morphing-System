using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseState
{
    public List<ISubscribeToBattleStateChanged> TickSubscribers { get; }
    public List<ISubscribeToBattleStateChanged> OnEnterSubscribers { get; }
    public List<ISubscribeToBattleStateChanged> OnExitSubscribers { get; }

    protected BaseState()
    {
        TickSubscribers = new List<ISubscribeToBattleStateChanged>();
        OnEnterSubscribers = new List<ISubscribeToBattleStateChanged>();
        OnExitSubscribers = new List<ISubscribeToBattleStateChanged>();
    }
    
    public virtual Task Tick()
    {
        foreach (var subscriber in TickSubscribers)
        {
            subscriber.Tick();
        }
        return Task.CompletedTask;
    }

    public virtual Task OnEnter()
    {
        Debug.Log("Entered state: " + GetType().Name);
        foreach (var subscriber in OnEnterSubscribers)
        {
            subscriber.OnEnter();
        }
        return Task.CompletedTask;
    }

    public virtual Task OnExit()
    {
        Debug.Log("Exit state: " + GetType().Name);
        foreach (var subscriber in OnExitSubscribers)
        {
            subscriber.OnExit();
        }
        return Task.CompletedTask;
    }
}