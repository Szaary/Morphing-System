using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour , ICharacterSystem
{
    [Header("Do not set anything here, will be changed ad app start.")]
    public Character character;

    private CharacterFacade _facade;

    public void SetCharacter(CharacterFacade characterFacade, Character template)
    {
        _facade = characterFacade;
        character = template.Clone();
        character.CreateInstances();
        SetFaction(character.Alignment);
        
        character.active.Initialize();
        ApplyStartupPassives();
        ApplyStartupEffects();
        ApplyStartupItems();
        
        _facade.CharacterSystems.Add(this);
    }

    public void SetFaction(Alignment characterAlignment)
    {
        _facade.gameObject.layer = characterAlignment.FactionLayerMask;
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
    
    public Result ActivateEffect(Effect effect)
    {
        var result = effect.ActivateEffect();
        if (result == Result.ToDestroy)
        {
            _facade.stats.character.RemoveEffect(effect);
            Destroy(effect);
            result = Result.Success;
        }
        return result;
    }


    private Result ApplyStartupItems()
    {
        foreach (var item in character.equipment.items)
        {
            var result = item.ApplyStatus(_facade, _facade);
            if (result != Result.Success)
            {
                HandlePassivesAddingError(result);
            }
        }

        return Result.Success;
    }


    public void ApplyEffect(Effect effect)
    {
        var status = effect.Clone();

        character.AddEffect(status);
        var result = status.ApplyStatus(_facade, _facade);
        if (result != Result.Success)
        {
            HandlePassivesAddingError(result);
        }
    }

    public void ApplyPassive(Passive passive)
    {
        var status = passive.Clone();

        character.AddPassive(status);
        var result = status.ApplyStatus(_facade, _facade);
        if (result != Result.Success)
        {
            HandlePassivesAddingError(result);
        }
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
        Debug.LogError(typeof(TurnStatsManager) + " apply passive result: " + result);
    }

    public void Disable()
    {
        enabled = false;
    }
    
    private void OnDestroy()
    {
        character.RemoveInstances();
        Destroy(character);
    }
}
