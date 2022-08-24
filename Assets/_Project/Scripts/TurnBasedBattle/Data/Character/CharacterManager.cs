using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private CharacterFacade facade;
    [HideInInspector] public Character character;

    public void SetCharacter(Character characterTemplate)
    {
        character = characterTemplate.Clone();
        character.CreateInstances();

        ApplyPassiveAbilities();
        ApplyStartupEffects();
        ApplyStartupItems();
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
        character.RemoveInstances();
        Destroy(character);
    }


    private Result ApplyPassiveAbilities()
    {
        foreach (var passiveEffect in character.passiveAbilities)
        {
            var result = ((IApplyPersistentStatus) passiveEffect).OnApplyStatus(facade, facade);
            if (result != Result.Success)
            {
                HandlePassivesAddingError(result);
            }
        }

        return Result.Success;
    }
    private Result ApplyStartupItems()
    {
        foreach (var item in character.equipment.items)
        {
            var result = ((IApplyPersistentStatus) item).OnApplyStatus(facade, facade);
            if (result != Result.Success)
            {
                HandlePassivesAddingError(result);
            }
        }
        return Result.Success;
    }

    
    private Result ApplyStartupEffects()
    {
        foreach (var effect in character.effects)
        {
            var result = effect.ApplyStatus(facade, facade);
            if (result != Result.Success)
            {
                HandlePassivesAddingError(result);
            }
        }

        return Result.Success;
    }
    
    private void HandlePassivesAddingError(Result result)
    {
        Debug.LogError(typeof(CharacterManager) + " apply passive result: " + result);
    }
}