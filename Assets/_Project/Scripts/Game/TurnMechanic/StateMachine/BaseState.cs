using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseState
{
    protected readonly TurnStateMachine _stateMachine;
    private bool isSilent = false;

    public List<TurnsSubscriber> TickSubscribers { get; }
    public List<TurnsSubscriber> OnEnterSubscribers { get; }
    public List<TurnsSubscriber> OnExitSubscribers { get; }

    protected BaseState(TurnStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        TickSubscribers = new List<TurnsSubscriber>();
        OnEnterSubscribers = new List<TurnsSubscriber>();
        OnExitSubscribers = new List<TurnsSubscriber>();
        stateMachine.States.Add(this);
    }

    public abstract void Tick();
    public abstract void OnEnter();
    public abstract void OnExit();


    protected void OnExitBaseImplementation()
    {
        if (!isSilent)
            Debug.Log("Ended state: " + GetType().Name + " Number of state subscribers: " + OnExitSubscribers.Count);
        foreach (var subscriber in OnExitSubscribers)
        {
            var result = subscriber.OnExit();
            HandleSubscriberResult(result, subscriber);
        }
    }

    protected void TickBaseImplementation()
    {
        foreach (var subscriber in TickSubscribers)
        {
            var result = subscriber.Tick();
            HandleSubscriberResult(result, subscriber);
        }
    }

    protected void OnEnterBaseImplementation()
    {
        if (!isSilent)
            Debug.Log("Entered state: " + GetType().Name + " Number of state subscribers: " + OnEnterSubscribers.Count);
        foreach (var subscriber in OnEnterSubscribers)
        {
            var result = subscriber.OnEnter();
            HandleSubscriberResult(result, subscriber);
        }
    }

    protected void HandleSubscriberResult(Result result, TurnsSubscriber subscriber)
    {
        if (result == Result.ToDestroy)
        {
            TickSubscribers.Remove(subscriber);
            OnEnterSubscribers.Remove(subscriber);
            OnExitSubscribers.Remove(subscriber);
            if (subscriber == null) return;
            if (!isSilent) Debug.Log("Destroying effect: " + subscriber);
        }
    }
}