using System.Threading.Tasks;

public class PlayerTurn : BaseState
{
    private bool _hasAnyoneActions;
    private readonly CharactersLibrary _charactersLibrary;

    public PlayerTurn(TurnStateMachine stateMachine, CharactersLibrary charactersLibrary) : base(stateMachine)
    {
        _charactersLibrary = charactersLibrary;
    }

    public override void Tick()
    {
        _hasAnyoneActions = false;

        for (var index = TickSubscribers.Count - 1; index >= 0; index--)
        {
            var subscriber = TickSubscribers[index];
            var result = subscriber.Tick();
            HandleSubscriberResult(result, subscriber);

            if (subscriber is IDoActions { ActionPoints: > 0 })
            {
                _hasAnyoneActions = true;
            }
        }

        if (_hasAnyoneActions == false)
        {
            _stateMachine.SetState(TurnState.AiTurn);
            return;
        }
    }
    
    public override void OnEnter()
    {
        OnEnterBaseImplementation();
    }

    public override void OnExit()
    {
        OnExitBaseImplementation();
    }
}