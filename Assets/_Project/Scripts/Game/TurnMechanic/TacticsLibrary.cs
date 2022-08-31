using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TacticsLibrary
{
    public static void RandomAttack(TurnBasedStrategy.CurrentFightState currentFightState)
    {
        var result = currentFightState.Character.ActiveSkillsManager.GetRandomAttack(currentFightState.Points, out Active skill);
        if (result == Result.Success)
        {
            if (skill.IsMultiTarget())
            {
                var targets = currentFightState.Library.SelectAllEnemies(currentFightState.Character.Alignment);
                foreach (var target in targets)
                {
                    skill.ActivateEffect(target, currentFightState.Character);
                }
            }
            else
            {
                var target = currentFightState.Library.SelectRandomEnemy(currentFightState.Character.Alignment.id);
                skill.ActivateEffect(target, currentFightState.Character);
            }
            currentFightState.ChangeActionPoints(skill.actions);
        }
        else
        {
            Debug.Log(result);
            currentFightState.ChangeActionPoints();
        }
    }
    
    public static List<CharacterFacade> GetPossibleActionsByPlayer(Active selectedActive, List<CharacterFacade> possibleTargets)
    {
        if (selectedActive.IsAttack())
        {
            return possibleTargets.Where(x => x.Alignment.id != 0).ToList();
        }
        else if (selectedActive.IsDefensive())
        {
            return possibleTargets.Where(x => x.Alignment.id == 0).ToList();
        }
        return new List<CharacterFacade>();
    }
    
    public static Result GetPossibleActionsToTakeByPlayer(TurnBasedStrategy.CurrentFightState currentFightState, out List<Active> actives,  out List<CharacterFacade> targets)
    {
        var offensiveSkillsResult = currentFightState.Character.ActiveSkillsManager.GetAttacks(currentFightState.Points, out var offense);
        var defensiveSkillsResult = currentFightState.Character.ActiveSkillsManager.GetDefensive(currentFightState.Points, out var defense);
        var allSkillsResult = currentFightState.Character.ActiveSkillsManager.GetActions(currentFightState.Points, out var all);


        var offensiveSkillsArePresent = offensiveSkillsResult == Result.Success;
        var defensiveSkillsArePresent = defensiveSkillsResult == Result.Success;
        var allSkillsArePresent = offensiveSkillsArePresent && defensiveSkillsArePresent;
        var anySkillsArePresent = allSkillsResult == Result.Success;
        
        
        var enemiesArePresent = currentFightState.Library.AiCharacters != 0;
        var alliesArePresent = currentFightState.Library.PlayerCharacters != 0;
        var bothEnemiesAndAllies = enemiesArePresent && alliesArePresent;

        if (!anySkillsArePresent) ReportError(out actives, out targets, Result.SkillsListIsEmpty);
        if (!bothEnemiesAndAllies) ReportError(out actives, out targets, Result.NoSuitableSkillsToUse);

        if (allSkillsArePresent && bothEnemiesAndAllies)
        {
            targets = currentFightState.Library.SelectAll();
            actives = all;
            Debug.Log("Selected tactic for targets: " + targets.Count + ". Skills count: " + actives.Count);
            return Result.Success;
        }

        if (offensiveSkillsArePresent && enemiesArePresent)
        {
            Debug.Log("No allies found, selecting only offensive skills");
            targets = currentFightState.Library.SelectAllEnemies(currentFightState.Character.Alignment);
            actives = offense;
            return Result.Success;
        }

        if (alliesArePresent && defensiveSkillsArePresent)
        {
            Debug.Log("No enemies found, selecting only defensive skills");
            targets = currentFightState.Library.SelectAllAllies(currentFightState.Character.Alignment);
            actives = defense;
            return Result.Success;
        }

        return ReportError(out actives, out targets, Result.NoSuitableSkillsToUse);
    }

    private static Result ReportError(out List<Active> actives, out List<CharacterFacade> targets, Result result)
    {
        Debug.Log("Tactic was not chosen, result: "+ result);
        targets = new List<CharacterFacade>();
        actives = new List<Active>();
        return result ;
    }

 
}