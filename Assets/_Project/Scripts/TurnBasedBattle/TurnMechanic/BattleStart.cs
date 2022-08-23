using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleStart : BaseState
{
    private readonly TurnStateMachine _turnStateMachine;
    public bool AreUnitsSpawned;
    
    public BattleStart(TurnStateMachine turnStateMachine)
    {
        _turnStateMachine = turnStateMachine;
    }

    public override async Task OnEnter()
    {
        await TickBaseImplementation();
    }
    
    public override async Task Tick()
    {
        await TickBaseImplementation();
        if(AreUnitsSpawned) StartBattle();
    }

    public override async Task OnExit()
    {
        await OnExitBaseImplementation();
    }
    
    private void StartBattle()
    {
        _turnStateMachine.SetState(TurnStateMachine.TurnState.PlayerTurn);
    }
}