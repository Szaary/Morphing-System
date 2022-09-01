using UnityEngine;

[CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/BaseRealTimeAiStrategy")]
public class BaseAiRealTimeStrategy : RealTimeStrategy
{
    public override Result OnEnter(CurrentFightState currentFightState)
    {
        var result = currentFightState.Library.GetControlledCharacter(out var controlled);
        if (result == Result.Success)
        {
            if (currentFightState.Agent.enabled)
            {
                currentFightState.Agent.SetDestination(controlled.transform.position);
            }
        }
        return Result.Success;
    }

    public override Result OnExit(CurrentFightState currentFightState)
    {
        return Result.Success;
    }

    public override Result Tick(CurrentFightState currentFightState)
    {
        return Result.Success;
    }
}