using System;
using System.Threading.Tasks;

//[CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/PlayerStrategy")]
public class PlayerStrategy : Strategy
{
    public override Task OnEnter(ChangeActionPointsDelegate removeActionPointsDelegate,
        CurrentFightState currentFightState)
    {
        return Task.CompletedTask;
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
