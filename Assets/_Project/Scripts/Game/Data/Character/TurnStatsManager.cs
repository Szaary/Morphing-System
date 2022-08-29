public class TurnStatsManager : TurnsSubscriber
{
    private CharacterFacade _facade;

    public void SetCharacter(CharacterFacade characterFacade)
    {
        _facade = characterFacade;

        SubscribeToStateChanges(_facade.Turns.PlayerTurn);
        SubscribeToStateChanges(_facade.Turns.AiTurn);
    }


    public override Result OnEnter()
    {
        var result = Result.Success;
        for (var index = _facade.manager.character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = _facade.manager.character.Effect[index];
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

        for (var index = _facade.manager.character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = _facade.manager.character.Effect[index];
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
            return _facade.manager.ActivateEffect(effect);
        }
        return Result.Success;
    }


    private void OnDestroy()
    {
        UnsubscribeFromStates();
    }
}