using System.Collections.Generic;
using System.Threading.Tasks;

public class Defeat : BaseState
{
    public Defeat(TurnStateMachine stateMachine) : base(stateMachine)
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