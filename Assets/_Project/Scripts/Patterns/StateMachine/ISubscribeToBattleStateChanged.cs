using System.Threading.Tasks;

public interface ISubscribeToBattleStateChanged
{
    BaseState BaseState { get; }

    /// <summary>
    /// To use Interface you need to call this method after you get reference to state.
    /// ((ISubscribeToStateChanged)this).SubscribeToStateChanges();
    /// </summary>
    void SubscribeToStateChanges()
    {
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