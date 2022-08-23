using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
public delegate void ChangeActionPointsDelegate(int points = TurnController.Settings.MAXActionPoints);

public class TurnController : MonoBehaviour, ISubscribeToBattleStateChanged, IDoActions
{
    
    private ChangeActionPointsDelegate _changeActionPointsDelegate;
    
    
    private Settings _settings;
    public int ActionPoints { get; private set; }

    public BaseState BaseState { get; private set; }
    private PlayerTurn _playerTurn;
    private AiTurn _aiTurn;

    private Strategy _strategy;
    private TurnBasedInput _input;
    private CharactersLibrary _library;
    private CharacterFacade facade;


    public void InitializeStrategy(Character.InitializationArguments arguments, Character character)
    {
        SetTurns(arguments, character);
        SetStrategy(character);
        ((ISubscribeToBattleStateChanged) this).SubscribeToStateChanges();

        if (_strategy is PlayerStrategy player)
        {
            _input.playerStrategy = player;
        }
    }

    private void Awake()
    {
        facade = GetComponent<CharacterFacade>();
        _changeActionPointsDelegate  = ChangePointsPoints;
    }

    private void ChangePointsPoints(int points)
    {
        ActionPoints -= points;
        Debug.Log("Changed action points to: "+ ActionPoints);
    }


    [Inject]
    public void Construct(Settings settings,
        TurnBasedInput input,
        CharactersLibrary library)
    {
        _settings = settings;
        _input = input;
        _library = library;
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
            _playerTurn = (PlayerTurn) arguments.playerTurn;
        }
        else
        {
            BaseState = arguments.aiTurn;
            _aiTurn = (AiTurn) arguments.aiTurn;
        }
    }


    private void OnDisable()
    {
        ((ISubscribeToBattleStateChanged) this).UnsubscribeFromStateChanges();
    }


  

    public async Task<BaseState.Result> Tick()
    {
        if (_strategy == null) return BaseState.Result.StrategyNotSet;

        await _strategy.Tick(_changeActionPointsDelegate, CreateFightState());
        return BaseState.Result.Success;
    }

    public async Task<BaseState.Result> OnEnter()
    {
        if (_strategy == null) return BaseState.Result.StrategyNotSet;

        ActionPoints = _settings.maxNumberOfActions;

        await _strategy.OnEnter(_changeActionPointsDelegate, CreateFightState());
        return BaseState.Result.Success;
    }

    public async Task<BaseState.Result> OnExit()
    {
        if (_strategy == null) return BaseState.Result.StrategyNotSet;

        await _strategy.OnExit(_changeActionPointsDelegate, CreateFightState());
        return BaseState.Result.Success;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }


    private Strategy.CurrentFightState CreateFightState()
    {
        return new Strategy.CurrentFightState()
        {
            Character = facade,
            Points = ActionPoints,
            Library = _library
        };
    }


    [Serializable]
    public class Settings
    {
        public const int MAXActionPoints = 100; 
        
        [Range(1, MAXActionPoints)]
        public int maxNumberOfActions;
    }
}