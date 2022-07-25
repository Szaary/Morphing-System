using System.Threading.Tasks;

public interface ISubscribeToStateChanged
{
    BaseState BaseState { get; }
    
    /// <summary>
    /// To use Interface you need to call this method after you get reference to state.
    /// </summary>
    void SubscribeToStateChanges()
    {
        SubscribeToOnEnter();
        SubscribeToTick();
        SubscribeToOnExit();
    }
    
    Task Tick();
    Task OnEnter();
    Task OnExit();
    
    
    private void SubscribeToTick()
    {
        BaseState.TickSubscribers.Add(this);
    }
    private void SubscribeToOnEnter()
    {
        BaseState.OnEnterSubscribers.Add(this);
    }
    private void SubscribeToOnExit()
    {
        BaseState.OnExitSubscribers.Add(this);
    }
}