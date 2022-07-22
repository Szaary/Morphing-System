using System.Collections.Generic;

public class PlayerTurn : IState
{
    public List<ISubscribeToStateChanged> TickSubscribers { get; }
    public List<ISubscribeToStateChanged> OnEnterSubscribers { get; }
    public List<ISubscribeToStateChanged> OnExitSubscribers { get; }

    public PlayerTurn()
    {
        TickSubscribers = new List<ISubscribeToStateChanged>();
        OnEnterSubscribers = new List<ISubscribeToStateChanged>();
        OnExitSubscribers = new List<ISubscribeToStateChanged>();
    }
}
