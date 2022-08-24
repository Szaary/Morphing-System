using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleStart : BaseState
{
    private readonly CharactersLibrary _charactersLibrary;

    public BattleStart(TurnStateMachine turnStateMachine, CharactersLibrary charactersLibrary) : base(turnStateMachine)
    {
        _charactersLibrary = charactersLibrary;
    }

    public override async Task OnEnter()
    {
        await TickBaseImplementation();
    }
    
    public override async Task Tick()
    {
        await TickBaseImplementation();
        if(_charactersLibrary.SpawnedAllCharacters) StartBattle();
    }

    public override async Task OnExit()
    {
        await OnExitBaseImplementation();
    }
    
    private void StartBattle()
    {
        _stateMachine.SetState(TurnState.PlayerTurn);
    }
}