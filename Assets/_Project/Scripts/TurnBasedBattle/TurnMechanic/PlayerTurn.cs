using System.Threading.Tasks;

public class PlayerTurn : BaseState
{
    private bool _hasAnyoneActions;
    private readonly CharactersLibrary _charactersLibrary;

    public PlayerTurn(TurnStateMachine stateMachine, CharactersLibrary charactersLibrary) : base(stateMachine)
    {
        _charactersLibrary = charactersLibrary;
    }

    public override async Task Tick()
    {
        _hasAnyoneActions = false;
        
        foreach (var subscriber in TickSubscribers)
        {
            var result = await subscriber.Tick();
            HandleSubscriberResult(result, subscriber);

            if (subscriber is IDoActions {ActionPoints: > 0})
            {
                _hasAnyoneActions = true;
            }
        }

        if (_charactersLibrary.PlayerCharacters == 0)
        {
            _stateMachine.SetState(TurnState.Defeat);
            return;
        }
        
        if (_charactersLibrary.AiCharacters == 0)
        {
            _stateMachine.SetState(TurnState.Victory);
            return;
        }
        
        if (_hasAnyoneActions == false)
        {
            _stateMachine.SetState(TurnState.AiTurn);
            return;
        }
    }
    
    public override async Task OnEnter()
    {
        await OnEnterBaseImplementation();
    }

    public override async Task OnExit()
    {
        await OnExitBaseImplementation();
    }
}