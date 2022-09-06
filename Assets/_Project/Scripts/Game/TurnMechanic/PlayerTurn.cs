using System.Threading.Tasks;
using UnityEngine;

public class PlayerTurn : BaseState
{
    private bool _hasAnyoneActions;

    public PlayerTurn(TurnStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Tick()
    {
        _hasAnyoneActions = false;

        for (var index = TickSubscribers.Count - 1; index >= 0; index--)
        {
            var subscriber = TickSubscribers[index];
            var result = subscriber.Tick();
            HandleSubscriberResult(result, subscriber);

            if (subscriber is IDoActions doActions)
            {
                if (doActions.ActionPoints > 0) _hasAnyoneActions = true;
            }
        }

        if (_hasAnyoneActions == false) _stateMachine.SetState(TurnState.AiTurn);
    }

    public override void OnEnter()
    {
        OnEnterBaseImplementation();
    }

    public override void OnExit()
    {
        OnExitBaseImplementation();
    }
}