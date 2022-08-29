using System.Collections.Generic;
using System.Threading.Tasks;

public class Victory : BaseState
{
    private readonly GameManager _gameManager;

    public Victory(TurnStateMachine stateMachine, GameManager gameManager) : base(stateMachine)
    {
        _gameManager = gameManager;
    }
    
    public override async Task Tick()
    {
        await TickBaseImplementation();
    }

    public override async Task OnEnter()
    {
        await OnEnterBaseImplementation();
        _gameManager.ChangeGameMode(GameMode.Fps);
    }

    public override async Task OnExit()
    {
        await OnExitBaseImplementation();
    }


}