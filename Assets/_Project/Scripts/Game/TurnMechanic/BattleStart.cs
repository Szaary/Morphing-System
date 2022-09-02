using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleStart : BaseState
{
    private readonly CharactersLibrary _charactersLibrary;
    private readonly GameManager _gameManager;
    private readonly TurnBasedInput _turnBasedInput;

    public BattleStart(TurnStateMachine turnStateMachine, 
        CharactersLibrary charactersLibrary,
        GameManager gameManager) : base(turnStateMachine)
    {
        _charactersLibrary = charactersLibrary;
        _gameManager = gameManager;
    }

    public override void OnEnter()
    {
       TickBaseImplementation();
    }
    
    public override void Tick()
    {
        TickBaseImplementation();
        if (!_charactersLibrary.SpawnedAllCharacters) return;
        if (_gameManager.GameMode != GameMode.TurnBasedFight) return;
        StartBattle();
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