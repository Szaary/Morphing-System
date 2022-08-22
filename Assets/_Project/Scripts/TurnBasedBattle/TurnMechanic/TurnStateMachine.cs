using System;
using UnityEngine;
using Zenject;

public class TurnStateMachine : MonoBehaviour
{
    public enum TurnState
    {
        BattleStart,
        PlayerTurn,
        AiTurn,
        Victory,
        Defeat
    }

    private TurnState _currentState;

    private AsyncStateMachine _battleStateMachine;
    private BattleStart _battleStart;
    private PlayerTurn _playerTurn;
    private AiTurn _aiTurn;
    private Victory _victory;
    private Defeat _defeat;

    [Inject]
    public void Construct(
        AsyncStateMachine battleStateMachine,
        BattleStart battleStart,
        PlayerTurn playerTurn,
        AiTurn aiTurn,
        Victory victory,
        Defeat defeat)
    {
        _battleStateMachine = battleStateMachine;
        _battleStart = battleStart;
        _playerTurn = playerTurn;
        _aiTurn = aiTurn;
        _victory = victory;
        _defeat = defeat;
    }

    private void Start()
    {
        At(_battleStart, _playerTurn, () => _currentState == TurnState.PlayerTurn);
        At(_playerTurn, _aiTurn, () => _currentState == TurnState.AiTurn);
        At(_aiTurn, _playerTurn, () => _currentState == TurnState.PlayerTurn);

        _battleStateMachine.AddAnyTransition(_victory, () => _currentState == TurnState.Victory);
        _battleStateMachine.AddAnyTransition(_defeat, () => _currentState == TurnState.Defeat);

        _battleStateMachine.SetState(_battleStart);

        void At(BaseState to, BaseState from, Func<bool> condition) => _battleStateMachine.AddTransition(to, from, condition);
    }
    
    protected void Update()
    {
        _battleStateMachine.Tick();
    }

    public void SetState(TurnState state)
    {
        Debug.Log("Changing state to " + state);
        _currentState = state;
    }

}