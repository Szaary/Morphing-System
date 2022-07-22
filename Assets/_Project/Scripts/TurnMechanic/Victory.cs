using System.Collections.Generic;

public class Victory : IState
{
    public Victory()
    {
        TickSubscribers = new List<ISubscribeToStateChanged>();
        OnEnterSubscribers = new List<ISubscribeToStateChanged>();
        OnExitSubscribers = new List<ISubscribeToStateChanged>();
    }

    public List<ISubscribeToStateChanged> TickSubscribers { get; }
    public List<ISubscribeToStateChanged> OnEnterSubscribers { get; }
    public List<ISubscribeToStateChanged> OnExitSubscribers { get; }
}