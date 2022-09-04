using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TacticsLibrary
{
    public enum Possible
    {
        OnlyDefensive,
        OnlyOffensive,
        Both,
        NoSuitableSkillsToUse,
        SkillsListIsEmpty
    }

    public static List<CharacterFacade> SelectTargetsBasedOnActive(Active selectedActive, List<CharacterFacade> possibleTargets, Alignment alignment)
    {
        if (selectedActive.IsAttack())
        {
            return possibleTargets.Where(x => x.Alignment.IsAlly(alignment)).ToList();
        }
        else if (selectedActive.IsDefensive())
        {
            return possibleTargets.Where(x => x.Alignment.IsAlly(alignment)).ToList();
        }

        return new List<CharacterFacade>();
    }

    public static Possible GetPossibleActions(TurnBasedStrategy.CurrentFightState currentFightState,
        out List<Active> actives, out List<CharacterFacade> targets)
    {
        var offensiveSkillsResult =
            currentFightState.Character.ActiveSkillsManager.GetAttacks(currentFightState.Points, out var offense);
        var defensiveSkillsResult =
            currentFightState.Character.ActiveSkillsManager.GetDefensive(currentFightState.Points, out var defense);
        var allSkillsResult =
            currentFightState.Character.ActiveSkillsManager.GetActions(currentFightState.Points, out var all);


        var offensiveSkillsArePresent = offensiveSkillsResult == Result.Success;
        var defensiveSkillsArePresent = defensiveSkillsResult == Result.Success;
        var allSkillsArePresent = offensiveSkillsArePresent && defensiveSkillsArePresent;
        var anySkillsArePresent = allSkillsResult == Result.Success;


        var enemiesArePresent = currentFightState.Library.AiCharacters != 0;
        var alliesArePresent = currentFightState.Library.PlayerCharacters != 0;
        var bothEnemiesAndAllies = enemiesArePresent && alliesArePresent;

        if (!anySkillsArePresent)
        {
            ReturnEmptyList(out actives, out targets);
            return Possible.SkillsListIsEmpty;
        }

        if (!bothEnemiesAndAllies)
        {
            ReturnEmptyList(out actives, out targets);
            return Possible.NoSuitableSkillsToUse;
        }

        if (allSkillsArePresent && bothEnemiesAndAllies)
        {
            targets = currentFightState.Library.SelectAll();
            actives = all;
            Debug.Log("Selected tactic for targets: " + targets.Count + ". Skills count: " + actives.Count);
            return Possible.Both;
        }

        if (offensiveSkillsArePresent && enemiesArePresent)
        {
            Debug.Log("No allies found, selecting only offensive skills");
            targets = currentFightState.Library.SelectAllEnemies(currentFightState.Character.Alignment);
            actives = offense;
            return Possible.OnlyOffensive;
        }

        if (alliesArePresent && defensiveSkillsArePresent)
        {
            Debug.Log("No enemies found, selecting only defensive skills");
            targets = currentFightState.Library.SelectAllAllies(currentFightState.Character.Alignment);
            actives = defense;
            return Possible.OnlyDefensive;
        }

        ReturnEmptyList(out actives, out targets);
        return Possible.NoSuitableSkillsToUse;
    }

    private static void ReturnEmptyList(out List<Active> actives, out List<CharacterFacade> targets)
    {
        targets = new List<CharacterFacade>();
        actives = new List<Active>();
    }
}