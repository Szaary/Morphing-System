
public class RealTimeStatsManager : RealtimeSubscriber
{
    public override Result OnEnter()
    {
        var result = Result.Success;
        for (var index = Facade.stats.character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = Facade.stats.character.Effect[index];
            if (effect.applyOnEnterCycle)
            {
                Facade.stats.ActivateEffect(effect);
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

        for (var index = Facade.stats.character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = Facade.stats.character.Effect[index];
            if (effect.applyOnExitCycle)
            {
                Facade.stats.ActivateEffect(effect);
            }
        }

        return result;
    }

    public override void Disable()
    {
        enabled = false;
    }
}