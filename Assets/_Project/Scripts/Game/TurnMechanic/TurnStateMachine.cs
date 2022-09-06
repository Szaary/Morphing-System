using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TurnStateMachine : MonoBehaviour
{
    private TurnState _currentState;

    private StateMachine _battleStateMachine;
    private BattleStart _battleStart;
    private PlayerTurn _playerTurn;
    private AiTurn _aiTurn;
    private Victory _victory;
    private Defeat _defeat;
    private GameManager _gameManager;
    private Paused _paused;

    private TurnState _lastState;

    public List<BaseState> States = new List<BaseState>();
    
    [Inject]
    public void Construct(
        GameManager gameManager,
        StateMachine battleStateMachine,
        BattleStart battleStart,
        PlayerTurn playerTurn,
        AiTurn aiTurn,
        Paused paused,
        Victory victory,
        Defeat defeat)
    {
        _gameManager = gameManager;
        _battleStateMachine = battleStateMachine;
        _battleStart = battleStart;
        _playerTurn = playerTurn;
        _aiTurn = aiTurn;
        _victory = victory;
        _defeat = defeat;
        _paused = paused;
    }

    private void Start()
    {
        _gameManager.GameModeChanged += OnGameModeChanged;
        
        At(_battleStart, _playerTurn, () => _currentState == TurnState.PlayerTurn);
        
        At(_playerTurn, _aiTurn, () => _currentState == TurnState.AiTurn);
        At(_aiTurn, _playerTurn, () => _currentState == TurnState.PlayerTurn);
        
        
        _battleStateMachine.AddAnyTransition(_victory, () => _currentState == TurnState.Victory);
        _battleStateMachine.AddAnyTransition(_defeat, () => _currentState == TurnState.Defeat);
        
        _battleStateMachine.AddAnyTransition(_paused, () => _currentState == TurnState.Paused);
        _battleStateMachine.AddAnyTransition(_battleStart, () => _currentState == TurnState.BattleStart);
        
        _battleStateMachine.SetState(_battleStart);

        void At(BaseState from, BaseState to, Func<bool> condition) =>
            _battleStateMachine.AddTransition(from, to, condition);
    }

    private void OnGameModeChanged(GameMode mode)
    {
        if (mode == GameMode.TurnBasedFight)
        {
            SetState(TurnState.BattleStart);
        }
        else
        {
            SetState(TurnState.Paused);
        }
    }

    protected void Update()
    {
        _battleStateMachine.Tick();
    }

    internal void SetState(TurnState state)
    {
        _currentState = state;
    }

    public TurnState GetCurrentState()
    {
        return _currentState;
    }

    
}