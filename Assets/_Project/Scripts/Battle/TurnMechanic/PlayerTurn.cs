using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerTurn : IState
{
    private readonly Settings _settings;
    private readonly TurnStateMachine _turnStateMachine;

    public List<ISubscribeToStateChanged> TickSubscribers { get; }
    public List<ISubscribeToStateChanged> OnEnterSubscribers { get; }
    public List<ISubscribeToStateChanged> OnExitSubscribers { get; }

    public PlayerTurn(Settings settings, TurnStateMachine turnStateMachine)
    {
        _settings = settings;
        _turnStateMachine = turnStateMachine;

        TickSubscribers = new List<ISubscribeToStateChanged>();
        OnEnterSubscribers = new List<ISubscribeToStateChanged>();
        OnExitSubscribers = new List<ISubscribeToStateChanged>();
    }

    public virtual Task Tick()
    {
        foreach (var subscriber in TickSubscribers)
        {
            subscriber.Tick();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _settings.currentActions = 0;
        }

        if (_settings.currentActions <= 0)
        {
            _turnStateMachine.SetState(TurnStateMachine.TurnState.AiTurn);
        }

        return Task.CompletedTask;
    }


    [Serializable]
    public class Settings
    {
        public int currentActions;
        public int maxNumberOfActions;
    }
}