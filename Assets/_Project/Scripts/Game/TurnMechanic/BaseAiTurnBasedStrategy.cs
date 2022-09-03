using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/BaseAiStrategy")]
public class BaseAiTurnBasedStrategy : TurnBasedStrategy
{
    public override Result SelectTactic(CurrentFightState currentFightState)
    {
        Debug.Log("Entered Ai turn, selecting move.");
        var result = TacticsLibrary.GetPossibleActions(currentFightState, out var active, out var targets);

        Debug.Log("Result: " + result);
        Debug.Log("Possible Actions count: " + active.Count);
        Debug.Log("Possible Targets count: " + targets.Count);
        
        if (result == TacticsLibrary.Possible.NoSuitableSkillsToUse) return Result.NoSuitableSkillsToUse;
        if (result == TacticsLibrary.Possible.Both)
        {
            var offensive = active.Where(x => x.IsAttack()).ToList();
            return ActivateRandomSkill(currentFightState, offensive, targets);
        }
        return ActivateRandomSkill(currentFightState, active, targets);
    }

    private static Result ActivateRandomSkill(CurrentFightState currentFightState, List<Active> active, List<CharacterFacade> targets)
    {
        var selectedSkillNumber = Random.Range(0, active.Count);
        var selectedSkill = active[selectedSkillNumber];
        var selectTargets = TacticsLibrary.SelectTargetsBasedOnActive(selectedSkill, targets, currentFightState.Character.Alignment.id);
        
        if (!selectedSkill.IsMultiTarget())
        {
            var randomTarget = Random.Range(0, selectTargets.Count);
            return selectedSkill.ActivateEffect(selectTargets[randomTarget], currentFightState.Character);
        }
        else
        {
            return selectedSkill.ActivateEffect(selectTargets, currentFightState.Character);
        }
    }
}