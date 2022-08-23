using System;
using System.Threading.Tasks;
using UnityEngine;

//CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/BaseAiStrategy")]
public class BaseAiStrategy : Strategy
{
    public override async Task OnEnter(TurnController.ChangeActionPointsDelegate removeActionPointsDelegate,
        CurrentFightState currentFightState)
    {
        TacticLibrary.RandomAttack(removeActionPointsDelegate, currentFightState);
    }

    public override Task OnExit(TurnController.ChangeActionPointsDelegate endTurn, CurrentFightState currentFightState)
    {
        return Task.CompletedTask;
    }

    public override Task Tick(TurnController.ChangeActionPointsDelegate endTurn, CurrentFightState currentFightState)
    {
        return Task.CompletedTask;
    }
    
    
    
}