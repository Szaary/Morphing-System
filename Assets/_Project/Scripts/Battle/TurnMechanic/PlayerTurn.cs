using System;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerTurn : BaseState, IDoActions
{
    private readonly Settings _settings;
    private readonly TurnStateMachine _turnStateMachine;

    
    public int CurrentActions { get; private set; }

    public PlayerTurn(Settings settings, TurnStateMachine turnStateMachine) : base()
    {
        _settings = settings;
        _turnStateMachine = turnStateMachine;
    }

    public override Task Tick()
    {
        foreach (var subscriber in TickSubscribers)
        {
            subscriber.Tick();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentActions = 0;
        }

        if (CurrentActions <= 0)
        {
            _turnStateMachine.SetState(TurnStateMachine.TurnState.AiTurn);
        }

        return Task.CompletedTask;
    }

    public override Task OnEnter()
    {
        Debug.Log("Entered state: " + GetType().Name);
        foreach (var subscriber in OnEnterSubscribers)
        {
            subscriber.OnEnter();
        }
        CurrentActions= _settings.maxNumberOfActions;   
        return Task.CompletedTask;
    }
    

    [Serializable]
    public class Settings
    {
        public int maxNumberOfActions;
    }

    
}