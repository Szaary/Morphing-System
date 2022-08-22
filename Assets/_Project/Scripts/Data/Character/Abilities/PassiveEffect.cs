using System;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// Passive effect effect last X number of turns.
/// </summary>
[CreateAssetMenu(fileName = "PE_", menuName = "Abilities/Passive Effect")]
public class PassiveEffect : Passive, IApplyStatusOverTurns, ISubscribeToBattleStateChanged
{
    [SerializeField] private int durationInTurns;
    [SerializeField] private bool applyOnEnterTurnState;
    [SerializeField] private bool applyOnExitTurnState;
    [SerializeField] private bool workOnOppositeTurn;
    
    public BaseState BaseState { get; private set; }

    public int DurationInTurns
    {
        get => durationInTurns;
        set => durationInTurns = value;
    }

    public Character Target { get; set; }
    public IOperateStats User { get; set; }

    public void SetState(Character character)
    {
        if (workOnOppositeTurn)
        {
            BaseState = character.alignment.alignment == 0 ? character.GetAiState() : character.GetPlayerState();
        }
        else
        {
            BaseState = character.GetState();
        }
        ((ISubscribeToBattleStateChanged) this).SubscribeToStateChanges();
    }

    private void OnDisable()
    {
        ((ISubscribeToBattleStateChanged) this).UnsubscribeFromStateChanges();
    }

    public async Task<BaseState.Result> OnEnter()
    {
        var result = BaseState.Result.Success;
        if (applyOnEnterTurnState)
        {
            result = ActivateEffect();
        }

        return result;
    }
    
    public async Task<BaseState.Result> Tick()
    {
        return BaseState.Result.Success;
    }

    public async Task<BaseState.Result> OnExit()
    {
        var result = BaseState.Result.Success;

        if (applyOnExitTurnState)
        {
            result = ActivateEffect();
        }

        return result;
    }

    public void Destroy()
    {
        Destroy(this);
    }

    private BaseState.Result ActivateEffect()
    {
        if (Target == null || User == null)
        {
            Debug.Log("Character or caller is null");
            return BaseState.Result.Failed;
        }

        Debug.Log("Activating effect by " + User.User.name + " on " + Target.data.characterName);
        var result = ((IApplyStatusOverTurns) this).TickStatus();

        if (result == IApplyStatusOverTurns.Result.HasEnded)
        {
            ((ISubscribeToBattleStateChanged) this).UnsubscribeFromStateChanges();
            return BaseState.Result.ToDestroy;
        }

        return BaseState.Result.Success;
    }
}