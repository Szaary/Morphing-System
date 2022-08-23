using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public delegate void ChangeActionPointsDelegate(int points = TurnController.Settings.MAXActionPoints);

public delegate Strategy.CurrentFightState GetFightStateDelegate();

public class TurnController : MonoBehaviour, ISubscribeToBattleStateChanged, IDoActions
{
    private ChangeActionPointsDelegate _changeActionPointsDelegate;
    private GetFightStateDelegate _getCurrentFightState;

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
            _input.ActivateAction = Activate;
        }
    }


    private void Activate(Active active, List<CharacterFacade> targets, int chosenTarget)
    {
        int points = 0;
        if (active.IsMultiTarget())
        {
            foreach (var target in targets)
            {
                Debug.Log("Activating effect: "+ active+ " on " + target + " by " + facade);
                points = active.ActivateEffect(target.GetCharacter(), facade.GetCharacter());
            }
        }
        else
        {
            var target = targets.First(x => x.zoneIndex == chosenTarget);
            Debug.Log("Activating effect: "+ active+ "on " + target);
            points = active.ActivateEffect(target.GetCharacter(), facade.GetCharacter());
            
        }

        ActionPoints -= points;
    }

    private void Awake()
    {
        facade = GetComponent<CharacterFacade>();
        _changeActionPointsDelegate = ChangePointsPoints;
        _getCurrentFightState = CreateFightState;
    }

    private void ChangePointsPoints(int points)
    {
        ActionPoints -= points;
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

        await _strategy.Tick();
        return BaseState.Result.Success;
    }

    public async Task<BaseState.Result> OnEnter()
    {
        if (_strategy == null) return BaseState.Result.StrategyNotSet;
        ActionPoints = _settings.maxNumberOfActions;

        await _strategy.OnEnter(CreateFightState());

        if (_strategy is PlayerStrategy player)
        {
            if (player.GetPossibleActions(_getCurrentFightState, out var active, out var targets) ==
                PlayerStrategy.Result.Success)
            {
                _input.possibleActives = active;
                _input.possibleTargets = targets;
                _input.ResetInputs();
            }
        }

        return BaseState.Result.Success;
    }

    public async Task<BaseState.Result> OnExit()
    {
        if (_strategy == null) return BaseState.Result.StrategyNotSet;

        await _strategy.OnExit(CreateFightState());
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
            Library = _library,
            Reset = _input.ResetInputs,
            ChangeActionPoints = _changeActionPointsDelegate
        };
    }


    [Serializable]
    public class Settings
    {
        public const int MAXActionPoints = 100;

        [Range(1, MAXActionPoints)] public int maxNumberOfActions;
    }
}