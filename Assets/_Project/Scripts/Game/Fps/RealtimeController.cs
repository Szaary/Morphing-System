public class RealtimeController : RealtimeSubscriber
{
    private CharacterFacade _facade;
    public void Initialize(CharacterFacade characterFacade)
    {
        _facade = characterFacade;
    }
    
    public override Result Tick()
    {
        return Result.Success;
    }

    public override Result OnEnter()
    {
        return Result.Success;
    }

    public override Result OnExit()
    {
        return Result.Success;
    }

  
}
