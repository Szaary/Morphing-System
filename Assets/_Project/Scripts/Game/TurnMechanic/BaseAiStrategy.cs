using System;
using System.Threading.Tasks;
using UnityEngine;

//CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/BaseAiStrategy")]
public class BaseAiStrategy : Strategy
{
    public override Result OnEnter(CurrentFightState currentFightState)
    {
        Debug.Log("Entered Ai turn, selecting move.");
        TacticsLibrary.RandomAttack(currentFightState);
        return Result.Success;
    }


    public override Result OnExit(CurrentFightState currentFightState)
    {
        return Result.Success;
    }

    public override Result Tick()
    {
        return Result.Success;
    }
}