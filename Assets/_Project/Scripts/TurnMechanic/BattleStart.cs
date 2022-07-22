
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : IState
{
    public List<ISubscribeToStateChanged> TickSubscribers { get; }
    public List<ISubscribeToStateChanged> OnEnterSubscribers { get; }
    public List<ISubscribeToStateChanged> OnExitSubscribers { get; }

    public BattleStart()
    {
        TickSubscribers = new List<ISubscribeToStateChanged>();
        OnEnterSubscribers = new List<ISubscribeToStateChanged>();
        OnExitSubscribers = new List<ISubscribeToStateChanged>();
    }
}
