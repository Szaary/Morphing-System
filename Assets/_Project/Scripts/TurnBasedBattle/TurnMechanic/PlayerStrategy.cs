using System;
using System.Threading.Tasks;

//[CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/PlayerStrategy")]
public class PlayerStrategy : Strategy
{
    public override Task OnEnter(Action endTurn)
    {
        return Task.CompletedTask;
    }

    public override Task OnExit(Action endTurn)
    {
        return Task.CompletedTask;
    }

    public override Task Tick(Action endTurn)
    {
        return Task.CompletedTask;
    }
}
