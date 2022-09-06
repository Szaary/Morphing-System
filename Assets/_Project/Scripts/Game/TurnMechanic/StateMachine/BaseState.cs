using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseState
{
    protected readonly TurnStateMachine _stateMachine;

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
        Debug.Log("Exit state: " + GetType().Name + " Number of state subscribers: " + OnExitSubscribers.Count);
        for (var index = OnExitSubscribers.Count - 1; index >= 0; index--)
        {
            var subscriber = OnExitSubscribers[index];
            var result = subscriber.OnExit();
            HandleSubscriberResult(result, subscriber);
        }
    }

    protected void TickBaseImplementation()
    {
        for (var index = TickSubscribers.Count - 1; index >= 0; index--)
        {
            var subscriber = TickSubscribers[index];
            var result = subscriber.Tick();
            HandleSubscriberResult(result, subscriber);
        }
    }

    protected void OnEnterBaseImplementation()
    {
        Debug.Log("----------------------------------------");
        Debug.Log("Entered state: " + GetType().Name + " Number of state subscribers: " + OnEnterSubscribers.Count);
        for (var index = OnEnterSubscribers.Count - 1; index >= 0; index--)
        {
            var subscriber = OnEnterSubscribers[index];
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
        }
    }
}