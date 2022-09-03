using UnityEngine;

public class TurnController : TurnsSubscriber, IDoActions
{
    public int ActionPoints { get; private set; }
    private CharacterFacade _facade;

    public void Initialize(CharacterFacade character)
    {
        _facade = character;
        if (character.Alignment.Id == 0)
            SubscribeToState(_facade.Turns.PlayerTurn);
        else
            SubscribeToState(_facade.Turns.AiTurn);
    }

    private void SubscribeToState(BaseState state)
    {
        SubscribeToStateChanges(state);
    }

    public override Result OnEnter()
    {
        ActionPoints = _facade.GetActionPoints();

        var result = _facade.GetTurnBasedStrategy().SelectTactic(CreateFightState());
        if (result != Result.Success)
        {
            Debug.LogError(typeof(TurnController) + " " + result);
        }

        return Result.Success;
    }

    public override Result Tick()
    {
        return Result.Success;
    }

    public override Result OnExit()
    {
        return Result.Success;
    }

    public void ApplyCost(int cost)
    {
        ActionPoints -= cost;
    }

    private TurnBasedStrategy.CurrentFightState CreateFightState()
    {
        return new TurnBasedStrategy.CurrentFightState()
        {
            Character = _facade,
            Library = _facade.Library,
            Points = ActionPoints,
            TurnBasedInputManager = _facade.BasedInputManager
        };
    }

    private void OnDestroy()
    {
        UnsubscribeFromStates();
    }
}