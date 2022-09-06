using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/BaseAiStrategy")]
public class BaseAiTurnBasedStrategy : TurnBasedStrategy
{
    public override Result SelectTactic(CurrentFightState currentFightState, out SelectedStrategy selectedStrategy)
    {
        Debug.Log("Selecting Ai strategy by: " + currentFightState.Character.name);
        var result = TacticsLibrary.GetPossibleActions(currentFightState, out var active, out var targets);
        Debug.Log("Possible Actions count: " + active.Count);
        Debug.Log("Possible Targets count: " + targets.Count);

        if (result == TacticsLibrary.Possible.NoSuitableSkillsToUse)
        {
            selectedStrategy = new SelectedStrategy();
            return Result.NoSuitableSkillsToUse;
        }

        if (result == TacticsLibrary.Possible.Both)
        {
            var offensive = active.Where(x => x.IsAttack()).ToList();
            return SelectSkillForTarget(currentFightState, offensive, targets, out selectedStrategy);
        }

        return SelectSkillForTarget(currentFightState, active, targets, out selectedStrategy);
    }

    private Result SelectSkillForTarget(CurrentFightState currentFightState,
        List<Active> active,
        List<CharacterFacade> targets,
        out SelectedStrategy strategy
    )
    {
        var selectedSkillNumber = Random.Range(0, active.Count);
        var selectedSkill = active[selectedSkillNumber];
        var selectTargets =
            TacticsLibrary.SelectTargetsBasedOnActive(selectedSkill, targets, currentFightState.Character.Alignment);

        if (!selectedSkill.IsMultiTarget())
        {
            var randomTarget = Random.Range(0, selectTargets.Count);
            var list = new List<CharacterFacade> {selectTargets[randomTarget]};
            strategy = new SelectedStrategy(currentFightState.Character, selectedSkill, list);
        }
        else
        {
            strategy = new SelectedStrategy(currentFightState.Character, selectedSkill, selectTargets);
        }

        return Result.Success;
    }
}