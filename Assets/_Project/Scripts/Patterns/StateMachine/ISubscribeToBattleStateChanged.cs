using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// To use Interface you need to call SubscribeToStateChanges() method after you get reference to state.
/// ((ISubscribeToStateChanged)this).SubscribeToStateChanges();
/// same thing with UnsubscribeFromStateChanges();
/// can be used by any effects that operate on turns.
/// </summary>
public interface ISubscribeToBattleStateChanged
{
    BaseState BaseState { get; }

    void SubscribeToStateChanges()
    {
        if (BaseState == null)
        {
            Debug.LogError("Base state is not set");
        }
        SubscribeToTick();
        SubscribeToOnEnter();
        SubscribeToOnExit();
    }

    void UnsubscribeFromStateChanges()
    {
        UnsubscribeFromTick();
        UnsubscribeFromOnEnter();
        UnsubscribeFromOnExit();
    }

    public Task<BaseState.Result> Tick();
    public Task<BaseState.Result> OnEnter();
    public Task<BaseState.Result> OnExit();

    
    void Destroy();

    public void SubscribeToTick()
    {
        BaseState?.TickSubscribers?.Add(this);
    }

    public void SubscribeToOnEnter()
    {
        BaseState?.OnEnterSubscribers?.Add(this);
    }

    public void SubscribeToOnExit()
    {
        BaseState?.OnExitSubscribers?.Add(this);
    }


    public void UnsubscribeFromTick()
    {
        BaseState?.TickSubscribers?.Remove(this);
    }

    public void UnsubscribeFromOnEnter()
    {
        BaseState?.OnEnterSubscribers?.Remove(this);
    }

    public void UnsubscribeFromOnExit()
    {
        BaseState?.OnExitSubscribers?.Remove(this);
    }
    
}