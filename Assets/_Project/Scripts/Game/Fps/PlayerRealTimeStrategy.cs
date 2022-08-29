using UnityEngine;

[CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/PlayerRealtimeStrategy")]
public class PlayerRealTimeStrategy : RealTimeStrategy
{
    public override Result OnEnter(CurrentFightState currentFightState)
    {
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