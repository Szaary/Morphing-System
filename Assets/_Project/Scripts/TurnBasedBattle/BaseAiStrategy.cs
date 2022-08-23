using System;
using System.Threading.Tasks;
using UnityEngine;

//CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/BaseAiStrategy")]
public class BaseAiStrategy : Strategy
{
    public override async Task OnEnter(CurrentFightState currentFightState)
    {
        TacticsLibrary.RandomAttack(currentFightState);
    }

    public override Task OnExit(CurrentFightState currentFightState)
    {
        return Task.CompletedTask;
    }

    public override Task Tick()
    {
        return Task.CompletedTask;
    }
}