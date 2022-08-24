using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TacticsLibrary
{
    public enum Result
    {
        Success,
        EmptySkillList,
        EmptyTargetList
    }
    
    public static void RandomAttack(Strategy.CurrentFightState currentFightState)
    {
        if (currentFightState.Character.Active.GetRandomAttack(currentFightState.Points, out Active skill) ==
            ActiveManager.Result.Success)
        {
            if (skill.IsMultiTarget())
            {
                var targets = currentFightState.Library.SelectAllEnemies(currentFightState.Character.Alignment);
                foreach (var target in targets)
                {
                    skill.ActivateEffect(target.GetCharacter(), currentFightState.Character.GetCharacter());
                }
            }
            else
            {
                var target = currentFightState.Library.SelectRandomEnemy(currentFightState.Character.Alignment);
                skill.ActivateEffect(target.GetCharacter(), currentFightState.Character.GetCharacter());
            }

            currentFightState.ChangeActionPoints(skill.actions);
        }
        else
        {
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
    
    public static Result GetPossibleActions(Strategy.CurrentFightState currentFightState, out List<Active> actives,  out List<CharacterFacade> targets)
    {
        if (currentFightState.Library.AiCharacters == 0)
        {
            if (currentFightState.Character.Active.GetDefensive(currentFightState.Points, out var defense) ==
                ActiveManager.Result.Success)
            {
                Debug.Log("Found skills");
                targets = currentFightState.Library.SelectAllAllies(currentFightState.Character.Alignment);
                actives = defense;
                return Result.Success;
            }
            return ReportError(out actives, out targets, Result.EmptySkillList);
        }

        if (currentFightState.Library.AiCharacters == 0)
        {
            if (currentFightState.Character.Active.GetAttacks(currentFightState.Points, out var offense) ==
                ActiveManager.Result.Success)
            {
                targets = currentFightState.Library.SelectAllEnemies(currentFightState.Character.Alignment);
                actives = offense;
                return Result.Success;
            }
            return ReportError(out actives, out targets, Result.EmptySkillList);
        }

        if (currentFightState.Character.Active.GetActions(currentFightState.Points, out var all) ==
            ActiveManager.Result.Success)
        {
            targets = currentFightState.Library.SelectAll();
            actives = all;
            return Result.Success;
        }
        return ReportError(out actives, out targets, Result.EmptyTargetList);
    }

    private static Result ReportError(out List<Active> actives, out List<CharacterFacade> targets, Result emptyTargetList)
    {
        targets = new List<CharacterFacade>();
        actives = new List<Active>();
        return emptyTargetList ;
    }

 
}