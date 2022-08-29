
public class RealTimeStatsManager : RealtimeSubscriber
{
    private CharacterFacade _facade;
    public void Initialize(CharacterFacade characterFacade)
    {
        _facade = characterFacade;
    }

    public override Result OnEnter()
    {
        var result = Result.Success;
        for (var index = _facade.manager.character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = _facade.manager.character.Effect[index];
            if (effect.applyOnEnterCycle)
            {
                _facade.manager.ActivateEffect(effect);
            }
        }

        return result;
    }

    public override Result Tick()
    {
        return Result.Success;
    }

    public override Result OnExit()
    {
        var result = Result.Success;

        for (var index = _facade.manager.character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = _facade.manager.character.Effect[index];
            if (effect.applyOnExitCycle)
            {
                _facade.manager.ActivateEffect(effect);
            }
        }

        return result;
    }


}