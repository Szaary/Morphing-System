using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// To use Interface you need to call SubscribeToStateChanges() method after you get reference to state.
/// ((ISubscribeToStateChanged)this).SubscribeToStateChanges();
/// same thing with UnsubscribeFromStateChanges();
/// can be used by any effects that operate on turns.
/// list of subscriptions : public List<BaseState> SubscribedTo { get; } = new();
/// </summary>
public interface ISubscribeToBattleStateChanged
{
    List<BaseState> SubscribedTo { get; set; }
    
    void SubscribeToStateChanges(BaseState state)
    {
        SubscribeToTick(state);
        SubscribeToOnEnter(state);
        SubscribeToOnExit(state);

        SubscribedTo ??= new List<BaseState>();
        SubscribedTo.Add(state);
    }

    Task<Result> Tick();
    Task<Result> OnEnter();
    Task<Result> OnExit();

    void Destroy();

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