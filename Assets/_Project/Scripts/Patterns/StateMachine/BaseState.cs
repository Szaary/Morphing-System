using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseState
{
    public List<ISubscribeToStateChanged> TickSubscribers { get; }
    public List<ISubscribeToStateChanged> OnEnterSubscribers { get; }
    public List<ISubscribeToStateChanged> OnExitSubscribers { get; }

    protected BaseState()
    {
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