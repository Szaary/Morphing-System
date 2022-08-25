using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleStart : BaseState
{
    private readonly CharactersLibrary _charactersLibrary;
    private readonly TurnBasedInput _turnBasedInput;

    public BattleStart(TurnStateMachine turnStateMachine, CharactersLibrary charactersLibrary, TurnBasedInput turnBasedInput) : base(turnStateMachine)
    {
        _charactersLibrary = charactersLibrary;
        _turnBasedInput = turnBasedInput;
    }

    public override async Task OnEnter()
    {
        _turnBasedInput.SetStateMachine(_stateMachine);
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