using UnityEngine;

/// <summary>
/// Passive effect effect last X number of turns and trigger on turns.
/// </summary>
[CreateAssetMenu(fileName = "PE_", menuName = "Abilities/Passive Effect")]
public class Effect : Status
{
    public int durationInTurns;
    public bool applyOnEnterTurnState;
    public bool applyOnExitTurnState;
    public bool workOnOppositeTurn;

    private CharacterFacade Target { get; set; }
    private CharacterFacade User { get; set; }

    public override Result ApplyStatus(CharacterFacade target, CharacterFacade user)
    {
        Target = target;
        User = user;

        Debug.Log("Applying status " + this + " to " + target);
        return Result.Success;
    }

    public override Result RemoveStatus(CharacterFacade target, CharacterFacade user)
    {
        return target.UnModify(user, Modifiers);
    }

    
    public Result ActivateEffect()
    {
        if (Target == null || User == null)
        {
            Debug.Log("Character or caller is null");
            return Result.ToDestroy;
        }

        Debug.Log("Activating effect by " + User.name + " on " + Target);
        var result = TickStatus();

        if (result == Result.HasEnded)
        {
            RemoveStatus(Target, User);
            return Result.ToDestroy;
        }

        return Result.Success;
    }

    private Result TickStatus()
    {
        Debug.Log("Status effect Tick" + name);
        foreach (var modifier in Modifiers)
        {
            Target.GetStatistic(modifier.statisticToModify, out var statistic);
            modifier.algorithm.Modify(statistic, modifier, User);
        }

        durationInTurns--;
        if (durationInTurns <= 0)
        {
            RemoveStatus(Target, User);
            return Result.HasEnded;
        }

        return Result.Success;
    }
}