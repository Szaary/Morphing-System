using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class TurnController : MonoBehaviour, ISubscribeToBattleStateChanged, IDoActions
{
    private Settings _settings;
    public int CurrentActions { get; private set; }
    public BaseState BaseState { get; private set; }
    private PlayerTurn _playerTurn;
    private AiTurn _aiTurn;
    
    private Strategy _strategy;
    public void InitializeStrategy(Character.InitializationArguments arguments, Character character)
    {
        SetTurns(arguments, character);
        SetStrategy(character);
        ((ISubscribeToBattleStateChanged)this).SubscribeToStateChanges();
    }

    private void SetStrategy(Character character)
    {
        _strategy = character.strategy;
    }

    private void SetTurns(Character.InitializationArguments arguments, Character character)
    {
        if (character.Alignment.Id == 0)
        {
            BaseState = arguments.playerTurn;
            _playerTurn = (PlayerTurn)arguments.playerTurn;
        }
        else
        {
            BaseState = arguments.aiTurn;
            _aiTurn = (AiTurn)arguments.aiTurn;
        }
    }

    
    [Inject]
    public void Construct(Settings settings)
    {
        _settings = settings;
    }

    private void OnDisable()
    {
        ((ISubscribeToBattleStateChanged)this).UnsubscribeFromStateChanges();
    }


    public void EndTurn() => CurrentActions = 0;
    
    public async Task<BaseState.Result> Tick()
    {
        if (_strategy == null) return BaseState.Result.StrategyNotSet;
        
        await _strategy.Tick(EndTurn);
        return BaseState.Result.Success;
    }

    public async Task<BaseState.Result> OnEnter()
    {
        if (_strategy == null) return BaseState.Result.StrategyNotSet;
        
        CurrentActions = _settings.maxNumberOfActions;

        await _strategy.OnEnter(EndTurn);
        return BaseState.Result.Success;
    }

    public async Task<BaseState.Result> OnExit()
    {
        if (_strategy == null) return BaseState.Result.StrategyNotSet;
        
        await _strategy.OnExit(EndTurn);
        return BaseState.Result.Success;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

   
    [Serializable]
    public class Settings
    {
        public int maxNumberOfActions;
    }


}