
public class RealTimeStatsManager : RealtimeSubscriber
{
    public override Result OnEnter()
    {
        var result = Result.Success;
        for (var index = Facade.manager.character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = Facade.manager.character.Effect[index];
            if (effect.applyOnEnterCycle)
            {
                Facade.manager.ActivateEffect(effect);
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

        for (var index = Facade.manager.character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = Facade.manager.character.Effect[index];
            if (effect.applyOnExitCycle)
            {
                Facade.manager.ActivateEffect(effect);
            }
        }

        return result;
    }
}