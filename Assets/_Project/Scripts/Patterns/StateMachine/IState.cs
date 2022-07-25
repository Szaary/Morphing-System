using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IState
{
    List<ISubscribeToStateChanged> TickSubscribers { get; }
    List<ISubscribeToStateChanged> OnEnterSubscribers { get; }
    List<ISubscribeToStateChanged> OnExitSubscribers { get; }
    
    
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