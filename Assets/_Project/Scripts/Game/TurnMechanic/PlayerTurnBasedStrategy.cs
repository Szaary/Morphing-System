using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/PlayerStrategy")]
public class PlayerTurnBasedStrategy : TurnBasedStrategy
{
    public override Result OnEnter(CurrentFightState currentFightState)
    {
        currentFightState.Character.GainControl();
        Debug.Log("Entered Player turn, selecting move.");
        currentFightState.Inputs.ResetInputs();
        var result = GetPossibleActions(currentFightState, out var active, out var targets); 
        if (result== Result.Success)
        {
            Debug.Log("Sending Possible Actions data to inputs.");
            Debug.Log("Possible Actions count: "+ active.Count);
            Debug.Log("Possible Targets count: "+ targets.Count);
            currentFightState.Inputs.PopulateCurrentState(active, targets, currentFightState.Character, currentFightState.Points);
        }
        else
        {
            Debug.Log(result);
        }

        return result;
    }

    public override Result OnExit(CurrentFightState currentFightState)
    {
        return Result.Success;
    }

    public override Result Tick()
    {
        return Result.Success;
    }

    public Result SelectActive(int index, List<Active> possibleActives, List<CharacterFacade> possibleTargets,
        out Active chosenActive, out List<CharacterFacade> chosenTargets)
    {
        chosenActive = possibleActives.First(x => x.position == index);
        chosenTargets = TacticsLibrary.GetPossibleActionsByPlayer(chosenActive, possibleTargets);

        return Result.Success;
    }

    public Result GetPossibleActions(CurrentFightState currentFightState,
        out List<Active> active, out List<CharacterFacade> targets)
    {
        var result = TacticsLibrary.GetPossibleActionsToTakeByPlayer(currentFightState, out active, out targets);
        if (result == Result.Success)
        {
            return Result.Success;
        }
        return Result.NoTarget;
    }
}