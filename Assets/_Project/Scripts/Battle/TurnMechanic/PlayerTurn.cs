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

    public override async Task Tick()
    {
        foreach (var subscriber in TickSubscribers)
        {
            var result = await subscriber.Tick();
            HandleSubscriberResult(result, subscriber);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentActions = 0;
        }

        if (CurrentActions <= 0)
        {
            _turnStateMachine.SetState(TurnStateMachine.TurnState.AiTurn);
        }
    }

    public override async Task OnEnter()
    {
        Debug.Log("Entered state: " + GetType().Name + " Number of state subscribers: " + OnEnterSubscribers.Count);
        foreach (var subscriber in OnEnterSubscribers)
        {
            var result =  await subscriber.OnEnter();
            HandleSubscriberResult(result, subscriber);
        }
        CurrentActions= _settings.maxNumberOfActions;
    }
    

    [Serializable]
    public class Settings
    {
        public int maxNumberOfActions;
    }

    
}