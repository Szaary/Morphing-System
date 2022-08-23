using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

[Serializable]
public struct ActiveManager
{
    public enum Result
    {
        Success,
        ListIsEmpty,
    }
    
    public List<Active> abilities;
    public List<Active> abilitiesInUnitLibrary;

    public Result GetRandomAttack(int points, out Active active)
    {
        var selected = abilities.Where(x => x.IsAttack(x) && x.actions>=points).ToList();
        if (selected.Count == 0)
        {
            active = null;
            return Result.ListIsEmpty;
        }
        var index = Random.Range(0, selected.Count);
        active = selected[index];
        return Result.Success;
    }

    
    public void Validate()
    {
        if (abilities.Count > 4)
        {
            abilities.RemoveRange(4, abilities.Count-4);
        }

        foreach (var ability in abilities)
        {
            if (!abilitiesInUnitLibrary.Contains(ability))
            {
                abilitiesInUnitLibrary.Add(ability);
            }
        }

        for (var index = abilitiesInUnitLibrary.Count - 1; index >= 0; index--)
        {
            var ability = abilitiesInUnitLibrary[index];
            if (ability == null) abilitiesInUnitLibrary.Remove(ability);
        }
    }
}