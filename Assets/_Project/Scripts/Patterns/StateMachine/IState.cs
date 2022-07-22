using System.Collections.Generic;

public interface IState
{
    List<ISubscribeToStateChanged> TickSubscribers { get; }
    List<ISubscribeToStateChanged> OnEnterSubscribers { get; }
    List<ISubscribeToStateChanged> OnExitSubscribers { get; }
    
    
    public virtual void Tick()
    {
        foreach (var subscriber in TickSubscribers)
        {
            subscriber.Tick();
        }
    }

    public virtual void OnEnter()
    {
        foreach (var subscriber in OnEnterSubscribers)
        {
            subscriber.OnEnter();
        }
    }

    public virtual void OnExit()
    {
        foreach (var subscriber in OnExitSubscribers)
        {
            subscriber.OnExit();
        }
    }
}

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

    void Tick();
    void OnEnter();
    void OnExit();
}