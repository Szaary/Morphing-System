using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public delegate void ChangeActionPointsDelegate(int points = Character.MAXActionPoints);

public delegate Strategy.CurrentFightState GetFightStateDelegate();

public class TurnController : TurnsSubscriber, IDoActions
{
    private ChangeActionPointsDelegate _changeActionPointsDelegate;
    private GetFightStateDelegate _getCurrentFightState;

    public int ActionPoints { get; private set; }

    private PlayerTurn _playerTurn;
    private AiTurn _aiTurn;
    private CharacterFacade _facade;

    private void Awake()
    {
        _changeActionPointsDelegate = ChangePointsPoints;
        _getCurrentFightState = CreateFightState;
    }
    
    public void Initialize(CharacterFacade character)
    {
        _facade = character;
        if (character.Alignment.Id == 0)
            SubscribeToState(_playerTurn);
        else
            SubscribeToState(_aiTurn);
    }

    private void Start()
    {
        SetStrategy(_facade);
    }

    private void SubscribeToState(BaseState state)
    {
        SubscribeToStateChanges(state);
    }

    private void SetStrategy(CharacterFacade character)
    {
        if (character.GetStrategy() is PlayerStrategy player)
        {
            _facade.playerInput.playerStrategy = player;
            _facade.playerInput.ActivateAction = Activate;
        }
    }

    public override async Task<Result> OnEnter()
    {
        ActionPoints = _facade.GetActionPoints();

        await _facade.GetStrategy().OnEnter(CreateFightState());

        if (_facade.GetStrategy() is PlayerStrategy player)
        {
            if (player.GetPossibleActions(_getCurrentFightState, out var active, out var targets) ==
                Result.Success)
            {
                _facade.playerInput.possibleActives = active;
                _facade.playerInput.possibleTargets = targets;
                _facade.playerInput.ResetInputs();
            }
        }

        return Result.Success;
    }


    public override async Task<Result> Tick()
    {
        await _facade.GetStrategy().Tick();
        return Result.Success;
    }


    public override async Task<Result> OnExit()
    {
        if (_facade.GetStrategy() == null) return Result.StrategyNotSet;

        await _facade.GetStrategy().OnExit(CreateFightState());
        return Result.Success;
    }


    private void Activate(Active active, List<CharacterFacade> targets, int chosenTarget)
    {
        var points = 0;
        if (active.IsMultiTarget())
        {
            foreach (var target in targets)
            {
                Debug.Log("Activating effect: " + active + " on " + target + " by " + _facade);
                points = active.ActivateEffect(target, _facade);
            }
        }
        else
        {
            var target = targets.First(x => x.GetZoneIndex() == chosenTarget);
            Debug.Log("Activating effect: " + active + "on " + target);
            points = active.ActivateEffect(target, _facade);
        }

        ActionPoints -= points;
    }
    
    private Strategy.CurrentFightState CreateFightState()
    {
        return new Strategy.CurrentFightState()
        {
            Character = _facade,
            Points = ActionPoints,
            Library = _facade.library,
            Reset = _facade.playerInput.ResetInputs,
            ChangeActionPoints = _changeActionPointsDelegate
        };
    }
    
    private void ChangePointsPoints(int points)
    {
        ActionPoints -= points;
    }
    
    private void OnDestroy()
    {
        UnsubscribeFromStates();
    }
}