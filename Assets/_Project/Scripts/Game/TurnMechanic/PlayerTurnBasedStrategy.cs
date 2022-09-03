using UnityEngine;

//[CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/PlayerStrategy")]
public class PlayerTurnBasedStrategy : TurnBasedStrategy
{
    public override Result SelectTactic(CurrentFightState currentFightState)
    {
        currentFightState.Character.GainControl();
        currentFightState.TurnBasedInputManager.ResetInputs();

        Debug.Log("Entered Player turn, selecting move.");
        
        var result = TacticsLibrary.GetPossibleActions(currentFightState, out var active, out var targets);
        if (result is TacticsLibrary.Possible.Both or TacticsLibrary.Possible.OnlyDefensive
            or TacticsLibrary.Possible.OnlyOffensive)
        {
            Debug.Log("Sending Possible Actions data to inputs. Result: "+ result);
            Debug.Log("Possible Actions count: " + active.Count);
            Debug.Log("Possible Targets count: " + targets.Count);
            currentFightState.TurnBasedInputManager.PopulateCurrentState(active, targets, currentFightState.Character,
                currentFightState.Points);
            return Result.Success;
        }
        Debug.Log(typeof(PlayerTurnBasedStrategy) + " " + result);
        return Result.NoSuitableSkillsToUse;
    }
}