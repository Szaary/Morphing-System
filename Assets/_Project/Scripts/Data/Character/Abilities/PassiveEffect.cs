using System;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// Passive effect effect last for some time.
/// </summary>
[CreateAssetMenu(fileName = "PE_", menuName = "Abilities/Passive Effect")]
public class PassiveEffect : Passive, IApplyStatusOverTime, ISubscribeToBattleStateChanged
{
    [SerializeField] private int durationInTurns;
    [SerializeField] private bool applyOnEnterState;
    [SerializeField] private bool applyOnExitState;
    
    public BaseState BaseState { get; private set; }
    
    public int DurationInTurns
    {
        get => durationInTurns;
        set => durationInTurns = value;
    }

    public Character Character { get; set; }
    public MonoBehaviour Caller { get; set; }
    public void SetState(Character character)
    {
        BaseState = character.GetState();
        ((ISubscribeToBattleStateChanged)this).SubscribeToStateChanges();
    }

    public Task Tick()
    {
        return Task.CompletedTask;
    }
    
    public async Task OnEnter()
    {
        if (applyOnEnterState)
        {
            await ActivateEffect();
        }
    }
    public async Task OnExit()
    {
        if (applyOnExitState)
        {
            await ActivateEffect();
        }
    }
    
    private Task ActivateEffect()
    {
        if (Character == null || Caller == null)
        {
            throw new Exception();
        }
        ((IApplyStatusOverTime)this).TickStatus();
        return Task.CompletedTask;
    }
}