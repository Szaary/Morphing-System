using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public delegate void ChangeActionPointsDelegate(int points = Character.MAXActionPoints);

public delegate Strategy.CurrentFightState GetFightStateDelegate();

public class TurnController : TurnsSubscriber, IDoActions
{
    private ChangeActionPointsDelegate _changeActionPointsDelegate;
    private GetFightStateDelegate _getCurrentFightState;

    public int ActionPoints { get; private set; }
    private CharacterFacade _facade;
    
    public void Initialize(CharacterFacade character)
    {
        _changeActionPointsDelegate = ChangePointsPoints;
        _getCurrentFightState = CreateFightState;
        
        _facade = character;
        if (character.Alignment.Id == 0)
            SubscribeToState(_facade.Turns.PlayerTurn);
        else
            SubscribeToState(_facade.Turns.AiTurn);
        
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
            Library = _facade.Library,
            Inputs = _facade.playerInput,
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