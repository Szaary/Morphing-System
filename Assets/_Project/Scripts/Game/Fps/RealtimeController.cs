using UnityEngine;

public class RealtimeController : RealtimeSubscriber
{
    private CharacterFacade _facade;
    
    
    public void Initialize(CharacterFacade characterFacade)
    {
        _facade = characterFacade;
    }

    public override Result Tick()
    {
        _facade.GetRealTimeStrategy().OnEnter(CreateFightState());
        return Result.Success;
    }

    public override Result OnEnter()
    {
        _facade.GetRealTimeStrategy().OnEnter(CreateFightState());
        return Result.Success;
    }

    public override Result OnExit()
    {
        _facade.GetRealTimeStrategy().OnEnter(CreateFightState());
        return Result.Success;
    }
    private RealTimeStrategy.CurrentFightState CreateFightState()
    {
        return new RealTimeStrategy.CurrentFightState()
        {
            Character = _facade,
            Library = _facade.Library,
            Agent = _facade.navMeshController
        };
    }

  
}
