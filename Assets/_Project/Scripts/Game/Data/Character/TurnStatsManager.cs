public class TurnStatsManager : TurnsSubscriber, ICharacterSystem
{
    private CharacterFacade _facade;

    public void Initialize(CharacterFacade characterFacade)
    {
        _facade = characterFacade;

        SubscribeToStateChanges(_facade.Turns.PlayerTurn);
        SubscribeToStateChanges(_facade.Turns.AiTurn);
        SubscribeToCharacterSystems();
    }
    public void SubscribeToCharacterSystems()
    {
        _facade.CharacterSystems.Add(this);
    }

    public override Result OnEnter()
    {
        var result = Result.Success;
        for (var index = _facade.stats.character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = _facade.stats.character.Effect[index];
            if (effect.applyOnEnterCycle)
            {
                result = ActivateEffectInTurn(effect);
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

        for (var index = _facade.stats.character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = _facade.stats.character.Effect[index];
            if (effect.applyOnExitCycle)
            {
                result = ActivateEffectInTurn(effect);
            }
        }

        return result;
    }

    private Result ActivateEffectInTurn(Effect effect)
    {
        if (_facade.Turns.ShouldWork(_facade, effect.workOnOppositeTurn))
        {
            return _facade.stats.ActivateEffect(effect);
        }
        return Result.Success;
    }


    private void OnDestroy()
    {
        UnsubscribeFromStates();
    }

    public void Disable()
    {
        UnsubscribeFromStates();
        enabled = false;
    }
}