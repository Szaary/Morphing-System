using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

//[CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/PlayerStrategy")]
public class PlayerStrategy : Strategy
{
    public enum Result
    {
        Success,
        NotEnoughEnergy,
        NoTarget,
        NoSkill
    }
    
    public override Task OnEnter(CurrentFightState currentFightState)
    {
        currentFightState.Reset();

        
        return Task.CompletedTask;
    }

    public override Task OnExit(CurrentFightState currentFightState)
    {
        return Task.CompletedTask;
    }

    public override Task Tick()
    {
        return Task.CompletedTask;
    }

    public Result SelectActive(int index, List<Active> possibleActives, List<CharacterFacade> possibleTargets, out Active chosenActive, out List<CharacterFacade> chosenTargets)
    {
        chosenActive = possibleActives.First(x => x.IndexOnBar == index);
        chosenTargets = TacticsLibrary.GetPossibleActionsByPlayer(chosenActive, possibleTargets);
        
        return Result.Success;
    }
    
    public Result GetPossibleActions(GetFightStateDelegate getFightStateDelegate, 
        out List<Active> active, out List<CharacterFacade> targets)
    {
        var state =  getFightStateDelegate();
        if (TacticsLibrary.GetPossibleActions(state, out active, out targets) == TacticsLibrary.Result.Success)
        {
            return Result.Success; 
        }
        return Result.NoTarget;
    }



}
