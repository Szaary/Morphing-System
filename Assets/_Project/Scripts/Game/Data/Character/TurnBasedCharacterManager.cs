using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TurnBasedCharacterManager : TurnsSubscriber
{
    private CharacterFacade _facade;

    public void SetCharacter(CharacterFacade characterFacade)
    {
        _facade = characterFacade;

        SubscribeToStateChanges(_facade.Turns.PlayerTurn);
        SubscribeToStateChanges(_facade.Turns.AiTurn);
        
        ApplyStartupPassives();
        ApplyStartupEffects();
        ApplyStartupItems();
    }


    public override async Task<Result> OnEnter()
    {
        var result = Result.Success;
        for (var index = _facade.manager.character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = _facade.manager.character.Effect[index];
            if (effect.applyOnEnterTurnState)
            {
                result = ActivateEffect(effect, result);
            }
        }

        return result;
    }

    public override async Task<Result> Tick()
    {
        return Result.Success;
    }

    public override async Task<Result> OnExit()
    {
        var result = Result.Success;

        for (var index = _facade.manager.character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = _facade.manager.character.Effect[index];
            if (effect.applyOnExitTurnState)
            {
                result = ActivateEffect(effect, result);
            }
        }

        return result;
    }


    public void ApplyEffect(Effect effect)
    {
        var status = effect.Clone();

        _facade.manager.character.AddEffect(status);
        var result = status.ApplyStatus(_facade, _facade);
        if (result != Result.Success)
        {
            HandlePassivesAddingError(result);
        }
    }

    public void ApplyPassive(Passive passive)
    {
        var status = passive.Clone();

        _facade.manager.character.AddPassive(status);
        var result = status.ApplyStatus(_facade, _facade);
        if (result != Result.Success)
        {
            HandlePassivesAddingError(result);
        }
    }

    public Result Modify(CharacterFacade user, List<Modifier> modifiers)
    {
        foreach (var modifier in modifiers)
        {
            _facade.manager. GetStatistic(modifier.statisticToModify, out var statistic);
            modifier.algorithm.Modify(statistic, modifier, user);
        }

        return Result.Success;
    }

    public Result UnModify(CharacterFacade user, List<Modifier> modifiers)
    {
        foreach (var modifier in modifiers)
        {
            _facade.manager.GetStatistic(modifier.statisticToModify, out var statistic);
            modifier.algorithm.UnModify(statistic, modifier, user);
        }

        return Result.Success;
    }


    private void OnDestroy()
    {
        UnsubscribeFromStates();
    }

    private Result ApplyStartupItems()
    {
        foreach (var item in _facade.manager.character.equipment.items)
        {
            var result = item.ApplyStatus(_facade, _facade);
            if (result != Result.Success)
            {
                HandlePassivesAddingError(result);
            }
        }

        return Result.Success;
    }


    private Result ActivateEffect(Effect effect, Result result)
    {
        if (_facade.Turns.ShouldWork(_facade, effect.workOnOppositeTurn))
        {
            result = effect.ActivateEffect();
            if (result == Result.ToDestroy)
            {
                _facade.manager.character.RemoveEffect(effect);
                Destroy(effect);
                result = Result.Success;
            }
        }

        return result;
    }

    private Result ApplyStartupEffects()
    {
        foreach (var effect in _facade.manager.character.templateEffects)
        {
            ApplyEffect(effect);
        }

        return Result.Success;
    }


    private Result ApplyStartupPassives()
    {
        foreach (var passive in _facade.manager.character.templatePassives)
        {
            ApplyPassive(passive);
        }

        return Result.Success;
    }


    private void HandlePassivesAddingError(Result result)
    {
        Debug.LogError(typeof(TurnBasedCharacterManager) + " apply passive result: " + result);
    }
}