using System;
using System.Threading.Tasks;
using UnityEngine;

//CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/BaseAiStrategy")]
public class BaseAiStrategy : Strategy
{
    public override async Task OnEnter(ChangeActionPointsDelegate removeActionPointsDelegate,
        CurrentFightState currentFightState)
    {
        TacticLibrary.RandomAttack(removeActionPointsDelegate, currentFightState);
    }

    public override Task OnExit(ChangeActionPointsDelegate endTurn, CurrentFightState currentFightState)
    {
        return Task.CompletedTask;
    }

    public override Task Tick(ChangeActionPointsDelegate endTurn, CurrentFightState currentFightState)
    {
        return Task.CompletedTask;
    }
    
    
    
}