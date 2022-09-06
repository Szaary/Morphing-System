public class Paused : BaseState
{
    private readonly GameManager _gameManager;

    public Paused(TurnStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Tick()
    {
        TickBaseImplementation();
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