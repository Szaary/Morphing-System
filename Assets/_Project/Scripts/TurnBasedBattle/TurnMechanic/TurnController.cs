using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public delegate void ChangeActionPointsDelegate(int points = Character.MAXActionPoints);

public delegate Strategy.CurrentFightState GetFightStateDelegate();

public class TurnController : MonoBehaviour, ISubscribeToBattleStateChanged, IDoActions
{
    public List<BaseState> SubscribedTo { get; set; }
    
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
        
        SetStrategy(_facade);
    }

    private void SubscribeToState(BaseState state)
    {
        ((ISubscribeToBattleStateChanged) this).SubscribeToStateChanges(state);
    }

    private void SetStrategy(CharacterFacade character)
    {
        if (character.GetStrategy() is PlayerStrategy player)
        {
            _facade.playerInput.playerStrategy = player;
            _facade.playerInput.ActivateAction = Activate;
        }
    }


    private void Activate(Active active, List<CharacterFacade> targets, int chosenTarget)
    {
        int points = 0;
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

  

    private void OnDestroy()
    {
        ((ISubscribeToBattleStateChanged) this).UnsubscribeFromStates();
    }

    private void ChangePointsPoints(int points)
    {
        ActionPoints -= points;
    }

    public async Task<Result> Tick()
    {
        await _facade.GetStrategy().Tick();
        return Result.Success;
    }

    public async Task<Result> OnEnter()
    {
        ActionPoints = _facade.GetActionPoints();

        await _facade.GetStrategy().OnEnter(CreateFightState());

        if (_facade.GetStrategy() is PlayerStrategy player)
        {
            if (player.GetPossibleActions(_getCurrentFightState, out var active, out var targets) ==
                PlayerStrategy.Result.Success)
            {
                _facade.playerInput.possibleActives = active;
                _facade.playerInput.possibleTargets = targets;
                _facade.playerInput.ResetInputs();
            }
        }

        return Result.Success;
    }

    public async Task<Result> OnExit()
    {
        if (_facade.GetStrategy() == null) return Result.StrategyNotSet;

        await _facade.GetStrategy().OnExit(CreateFightState());
        return Result.Success;
    }

    public void Destroy()
    {
        Destroy(gameObject);
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
}