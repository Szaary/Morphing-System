using UnityEngine;

public class RealtimeController : RealtimeSubscriber, ICharacterSystem
{
    public override Result Tick()
    {
        Facade.GetRealTimeStrategy().OnEnter(CreateFightState());
        return Result.Success;
    }

    public override Result OnEnter()
    {
        Facade.GetRealTimeStrategy().OnEnter(CreateFightState());
        return Result.Success;
    }

    public override Result OnExit()
    {
        Facade.GetRealTimeStrategy().OnEnter(CreateFightState());
        return Result.Success;
    }
    private RealTimeStrategy.CurrentFightState CreateFightState()
    {
        return new RealTimeStrategy.CurrentFightState()
        {
            Character = Facade,
            Library = Facade.Library,
            Agent = Facade.movement.navMeshAgentMovement
        };
    }

    public override void Disable()
    {
        enabled = false;
    }
}
