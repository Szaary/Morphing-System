using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterManager : TurnsSubscriber
{
    [SerializeField] private CharacterFacade facade;

    [Header("Do not set anything here, will be changed ad app start.")]
    public Character character;

    public void SetCharacter(Character characterTemplate)
    {
        character = characterTemplate.Clone();
        character.CreateInstances();

        ApplyStartupPassives();
        ApplyStartupEffects();
        ApplyStartupItems();

        SubscribeToStateChanges(facade.Turns.PlayerTurn);
        SubscribeToStateChanges(facade.Turns.AiTurn);
    }


    public override async Task<Result> OnEnter()
    {
        var result = Result.Success;
        for (var index = character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = character.Effect[index];
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

        for (var index = character.Effect.Count - 1; index >= 0; index--)
        {
            var effect = character.Effect[index];
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
        
        character.AddEffect(status);
        var result = status.ApplyStatus(facade, facade);
        if (result != Result.Success)
        {
            HandlePassivesAddingError(result);
        }
    }

    public void ApplyPassive(Passive passive)
    {
        var status = passive.Clone();
        
        character.AddPassive(status);
        var result = status.ApplyStatus(facade, facade);
        if (result != Result.Success)
        {
            HandlePassivesAddingError(result);
        }
    }

    public Result Modify(CharacterFacade user, List<Modifier> modifiers)
    {
        foreach (var modifier in modifiers)
        {
            GetStatistic(modifier.statisticToModify, out var statistic);
            modifier.algorithm.Modify(statistic, modifier, user);
        }

        return Result.Success;
    }

    public Result UnModify(CharacterFacade user, List<Modifier> modifiers)
    {
        foreach (var modifier in modifiers)
        {
            GetStatistic(modifier.statisticToModify, out var statistic);
            modifier.algorithm.UnModify(statistic, modifier, user);
        }

        return Result.Success;
    }

    public Result GetStatistic(BaseStatistic baseStatistic, out Statistic outStat)
    {
        foreach (var stat in character.statistics)
        {
            if (stat.baseStatistic == baseStatistic)
            {
                outStat = stat;
                return Result.Success;
            }
        }

        outStat = null;
        return Result.Failed;
    }

    private void OnDestroy()
    {
        UnsubscribeFromStates();
        character.RemoveInstances();
        Destroy(character);
    }

    private Result ApplyStartupItems()
    {
        foreach (var item in character.equipment.items)
        {
            var result = item.ApplyStatus(facade, facade);
            if (result != Result.Success)
            {
                HandlePassivesAddingError(result);
            }
        }

        return Result.Success;
    }

    
    private Result ActivateEffect(Effect effect, Result result)
    {
        if (facade.Turns.ShouldWork(facade, effect.workOnOppositeTurn))
        {
            result = effect.ActivateEffect();
            if (result == Result.ToDestroy)
            {
                character.RemoveEffect(effect);
                Destroy(effect);
                result = Result.Success;
            }
        }
        return result;
    }
    
    private Result ApplyStartupEffects()
    {
        foreach (var effect in character.templateEffects)
        {
            ApplyEffect(effect);
        }
        return Result.Success;
    }

    
    private Result ApplyStartupPassives()
    {
        foreach (var passive in character.templatePassives)
        {
            ApplyPassive(passive);
        }
        return Result.Success;
    }
    

    private void HandlePassivesAddingError(Result result)
    {
        Debug.LogError(typeof(CharacterManager) + " apply passive result: " + result);
    }
}