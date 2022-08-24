using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// Passive effect effect last X number of turns.
/// </summary>
[CreateAssetMenu(fileName = "PE_", menuName = "Abilities/Passive Effect")]
public class PassiveEffect : Passive, ISubscribeToBattleStateChanged
{
    public List<BaseState> SubscribedTo { get; set; }

    [SerializeField] private int durationInTurns;
    [SerializeField] private bool applyOnEnterTurnState;
    [SerializeField] private bool applyOnExitTurnState;
    [SerializeField] private bool workOnOppositeTurn;

    public int DurationInTurns
    {
        get => durationInTurns;
        set => durationInTurns = value;
    }

    private CharacterFacade Target { get; set; }
    private CharacterFacade User { get; set; }

    public Result ApplyStatus(CharacterFacade target, CharacterFacade user)
    {
        Target = target;
        User = user;

        ((ISubscribeToBattleStateChanged) this).SubscribeToStateChanges(user.GetPlayTurn(workOnOppositeTurn));

        Debug.Log("Applying status " + this + " to " + target);
        return Result.Success;
    }


    public async Task<Result> OnEnter()
    {
        var result = Result.Success;
        if (applyOnEnterTurnState)
        {
            result = ActivateEffect();
        }

        return result;
    }

    public async Task<Result> Tick()
    {
        return Result.Success;
    }

    public async Task<Result> OnExit()
    {
        var result = Result.Success;

        if (applyOnExitTurnState)
        {
            result = ActivateEffect();
        }

        return result;
    }



    private Result ActivateEffect()
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
            ((ISubscribeToBattleStateChanged) this).UnsubscribeFromStates();
            return Result.ToDestroy;
        }

        return Result.Success;
    }

    private Result TickStatus()
    {
        Debug.Log("Status effect Tick" + GetType().Name);
        foreach (var modifier in Modifiers)
        {
            Target.GetStatistic(modifier.statisticToModify, out var statistic);
            modifier.algorithm.Modify(statistic, modifier, User);
        }

        DurationInTurns--;
        if (DurationInTurns <= 0)
        {
            OnRemoveStatus(Target, User);
            return Result.HasEnded;
        }

        return Result.Success;
    }
    public void Destroy()
    {
        Destroy(this);
    }
    
    private Result OnRemoveStatus(CharacterFacade target, CharacterFacade user)
    {
        return target.UnModify(user, Modifiers);
    }

    private void OnDisable()
    {
        ((ISubscribeToBattleStateChanged) this).UnsubscribeFromStates();
    }
}