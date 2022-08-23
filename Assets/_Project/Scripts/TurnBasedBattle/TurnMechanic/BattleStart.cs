using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleStart : BaseState
{
    private readonly TurnStateMachine _turnStateMachine;
    private readonly CharacterFactory _characterFactory;

    public BattleStart(TurnStateMachine turnStateMachine, CharacterFactory characterFactory)
    {
        _turnStateMachine = turnStateMachine;
        _characterFactory = characterFactory;
    }

    public override async Task OnEnter()
    {
        await TickBaseImplementation();
    }
    
    public override async Task Tick()
    {
        await TickBaseImplementation();
        if(_characterFactory.SpawnedAllCharacters) StartBattle();
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