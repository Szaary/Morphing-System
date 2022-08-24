using System.Collections.Generic;
using System.Threading.Tasks;

public class Defeat : BaseState
{
    public Defeat(TurnStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override async Task Tick()
    {
        await  TickBaseImplementation();
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