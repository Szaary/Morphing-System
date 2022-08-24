using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public abstract class TurnsSubscriber : MonoBehaviour
{
    List<BaseState> SubscribedTo { get; set; }
    
    protected void SubscribeToStateChanges(BaseState state)
    {
        SubscribeToTick(state);
        SubscribeToOnEnter(state);
        SubscribeToOnExit(state);

        SubscribedTo ??= new List<BaseState>();
        SubscribedTo.Add(state);
    }

    public abstract Task<Result> Tick();
    public abstract Task<Result> OnEnter();
    public abstract Task<Result> OnExit();


    public void UnsubscribeFromStates()
    {
        if (SubscribedTo == null) return;
        for (var index = SubscribedTo.Count - 1; index >= 0; index--)
        {
            var state = SubscribedTo[index];
            UnsubscribeFromStateChanges(state);
            SubscribedTo.Remove(state);
        }
    }
    
    private void SubscribeToTick(BaseState baseState)
    {
        baseState?.TickSubscribers?.Add(this);
    }
    private void SubscribeToOnEnter(BaseState baseState)
    {
        baseState?.OnEnterSubscribers?.Add(this);
    }
    private void SubscribeToOnExit(BaseState baseState)
    {
        baseState?.OnExitSubscribers?.Add(this);
    }

    private void UnsubscribeFromStateChanges(BaseState state)
    {
        UnsubscribeFromTick(state);
        UnsubscribeFromOnEnter(state);
        UnsubscribeFromOnExit(state);
    }
    
    private void UnsubscribeFromTick(BaseState baseState)
    {
        baseState?.TickSubscribers?.Remove(this);
    }
    private void UnsubscribeFromOnEnter(BaseState baseState)
    {
        baseState?.OnEnterSubscribers?.Remove(this);
    }
    private void UnsubscribeFromOnExit(BaseState baseState)
    {
        baseState?.OnExitSubscribers?.Remove(this);
    }
}