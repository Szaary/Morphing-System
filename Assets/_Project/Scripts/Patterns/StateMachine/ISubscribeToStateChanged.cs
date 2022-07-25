using System.Threading.Tasks;

public interface ISubscribeToStateChanged
{
    IState State { get; }
    
    void SubscribeToTick()
    {
        State.TickSubscribers.Add(this);
    }
    void SubscribeToOnEnter()
    {
        State.OnEnterSubscribers.Add(this);
    }
    void SubscribeToOnExit()
    {
        State.OnExitSubscribers.Add(this);
    }

    Task Tick();
    Task OnEnter();
    Task OnExit();
}