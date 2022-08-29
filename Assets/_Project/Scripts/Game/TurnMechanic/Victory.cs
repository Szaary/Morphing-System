using System.Collections.Generic;
using System.Threading.Tasks;

public class Victory : BaseState
{
    private readonly GameManager _gameManager;

    public Victory(TurnStateMachine stateMachine, GameManager gameManager) : base(stateMachine)
    {
        _gameManager = gameManager;
    }
    
    public override void Tick()
    {
        TickBaseImplementation();
    }

    public override void OnEnter()
    {
        OnEnterBaseImplementation();
        _gameManager.SetGameMode(GameMode.Fps);
    }

    public override void OnExit()
    {
        OnExitBaseImplementation();
    }
}