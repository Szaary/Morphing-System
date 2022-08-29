using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleStart : BaseState
{
    private readonly CharactersLibrary _charactersLibrary;
    private readonly TurnBasedInput _turnBasedInput;

    public BattleStart(TurnStateMachine turnStateMachine, CharactersLibrary charactersLibrary) : base(turnStateMachine)
    {
        _charactersLibrary = charactersLibrary;
    }

    public override void OnEnter()
    {
       TickBaseImplementation();
    }
    
    public override void Tick()
    {
        TickBaseImplementation();
        if(_charactersLibrary.SpawnedAllCharacters) StartBattle();
    }

    public override void OnExit()
    {
        OnExitBaseImplementation();
    }
    
    private void StartBattle()
    {
        _stateMachine.SetState(TurnState.PlayerTurn);
    }
}