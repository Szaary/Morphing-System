using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseState
{
    private bool isSilent = true;
    public enum Result
    {
        Success,
        ToDestroy,
        Failed,
        StrategyNotSet
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

    public abstract Task Tick();
    public abstract Task OnEnter();
    public abstract Task OnExit();

    
    
    protected async Task OnExitBaseImplementation()
    {
        if(!isSilent) Debug.Log("Ended state: " + GetType().Name + " Number of state subscribers: " + OnExitSubscribers.Count);
        foreach (var subscriber in OnExitSubscribers)
        {
            var result = await subscriber.OnExit();
            HandleSubscriberResult(result, subscriber);
        }
    }
    protected async Task TickBaseImplementation()
    {
        foreach (var subscriber in TickSubscribers)
        {
            var result = await subscriber.Tick();
            HandleSubscriberResult(result, subscriber);
        }
    }

    protected async Task OnEnterBaseImplementation()
    {
        if(!isSilent) Debug.Log("Entered state: " + GetType().Name + " Number of state subscribers: " + OnEnterSubscribers.Count);
        foreach (var subscriber in OnEnterSubscribers)
        {
            var result = await subscriber.OnEnter();
            HandleSubscriberResult(result, subscriber);
        }
    }
    
    protected void HandleSubscriberResult(Result result, ISubscribeToBattleStateChanged subscriber)
    {
        if (result == Result.ToDestroy)
        {
            TickSubscribers.Remove(subscriber);
            OnEnterSubscribers .Remove(subscriber);
            OnExitSubscribers.Remove(subscriber);
            if(!isSilent) Debug.Log("Destroying effect: "+ subscriber);   
            subscriber.Destroy();
        }
    }
}