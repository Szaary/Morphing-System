using System.Threading.Tasks;

public interface ISubscribeToStateChanged
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
    
    Task Tick();
    Task OnEnter();
    Task OnExit();
    
    
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