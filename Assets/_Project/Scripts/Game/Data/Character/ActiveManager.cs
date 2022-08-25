using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct ActiveManager
{
    [SerializeField] private List<Active> abilities;
    [SerializeField] private List<Active> abilitiesInUnitLibrary;

    public Result GetRandomAttack(int points, out Active active)
    {
        var selected = abilities.Where(x => x.IsAttack() && x.actions>=points).ToList();
        if (selected.Count == 0)
        {
            active = null;
            return Result.SkillsListIsEmpty;
        }
        var index = Random.Range(0, selected.Count);
        active = selected[index];
        return Result.Success;
    }

    public Result GetDefensive(int points, out List<Active> actives)
    {
        var selected = abilities.Where(x => x.IsDefensive() && x.actions>=points).ToList();
        if (selected.Count == 0)
        {
            actives = new List<Active>();
            return Result.SkillsListIsEmpty;
        }

        actives = selected;
        return Result.Success;
    }
    
    public Result GetAttacks(int points, out List<Active> actives)
    {
        var selected = abilities.Where(x => x.IsAttack() && x.actions>=points).ToList();
        if (selected.Count == 0)
        {
            actives = new List<Active>();
            return Result.SkillsListIsEmpty;
        }

        actives = selected;
        return Result.Success;
    }
    
    public Result GetActions(int points, out List<Active> actives)
    {
        var selected = abilities.Where(x => x.actions>=points).ToList();
        if (selected.Count == 0)
        {
            actives = new List<Active>();
            return Result.SkillsListIsEmpty;
        }

        actives = selected;
        return Result.Success;
    }

    public void Validate()
    {
        if (abilities.Count > 4)
        {
            abilities.RemoveRange(4, abilities.Count-4);
        }

        for (var index = 0; index < abilities.Count; index++)
        {
            var ability = abilities[index];
            if (!abilitiesInUnitLibrary.Contains(ability))
            {
                abilitiesInUnitLibrary.Add(ability);
            }
            ability.position = index;
        }

        for (var index = abilitiesInUnitLibrary.Count - 1; index >= 0; index--)
        {
            var ability = abilitiesInUnitLibrary[index];
            if (ability == null) abilitiesInUnitLibrary.Remove(ability);
        }
    }
}