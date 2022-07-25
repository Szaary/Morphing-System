using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public abstract class BaseState
{
    public enum Result
    {
        Success,
        ToDestroy,
        Failed
    }

    public List<ISubscribeToBattleStateChanged> TickSubscribers { get; }
    public List<ISubscribeToBattleStateChanged> OnEnterSubscribers { get; }
    public List<ISubscribeToBattleStateChanged> OnExitSubscribers { get; }

    protected BaseState()
    {
        TickSubscribers = new List<ISubscribeToBattleStateChanged>();
        OnEnterSubscribers = new List<ISubscribeToBattleStateChanged>();
        OnExitSubscribers = new List<ISubscribeToBattleStateChanged>();
    }

    public virtual async Task Tick()
    {
        foreach (var subscriber in TickSubscribers)
        {
            var result = await subscriber.Tick();
            HandleSubscriberResult(result, subscriber);
        }
    }

    public virtual async Task OnEnter()
    {
        Debug.Log("Entered state: " + GetType().Name + " Number of state subscribers: " + OnEnterSubscribers.Count);
        foreach (var subscriber in OnEnterSubscribers)
        {
            var result = await subscriber.OnEnter();
            HandleSubscriberResult(result, subscriber);
        }
    }

    public virtual async Task OnExit()
    {
        Debug.Log("Entered state: " + GetType().Name + " Number of state subscribers: " + OnExitSubscribers.Count);
        foreach (var subscriber in OnExitSubscribers)
        {
            var result =  await subscriber.OnExit();
            HandleSubscriberResult(result, subscriber);
        }
    }

    protected void HandleSubscriberResult(Result result, ISubscribeToBattleStateChanged subscriber)
    {
        if (result == Result.ToDestroy)
        {
            subscriber.Destroy();
        }
    }
}