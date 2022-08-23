using System;
using System.Threading.Tasks;

//[CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/PlayerStrategy")]
public class PlayerStrategy : Strategy
{
    public override Task OnEnter(TurnController.ChangeActionPointsDelegate removeActionPointsDelegate,
        CurrentFightState currentFightState)
    {
        return Task.CompletedTask;
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
