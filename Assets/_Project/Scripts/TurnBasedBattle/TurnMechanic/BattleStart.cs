using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleStart : BaseState
{
    private readonly TurnStateMachine _turnStateMachine;

    public BattleStart(TurnStateMachine turnStateMachine)
    {
        _turnStateMachine = turnStateMachine;
    }

    public override Task OnEnter()
    {
        Debug.Log("Entered state: " + GetType().Name);
        foreach (var subscriber in OnEnterSubscribers)
        {
            subscriber.OnEnter();
        }

        _turnStateMachine.SetState(TurnStateMachine.TurnState.PlayerTurn);
        return Task.CompletedTask;
    }


    public override async Task Tick()
    {
        await TickBaseImplementation();
    }

    public override async Task OnExit()
    {
        await OnExitBaseImplementation();
    }
}