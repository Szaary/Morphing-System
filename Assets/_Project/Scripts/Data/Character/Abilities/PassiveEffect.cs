using System;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// Passive effect effect last for some time.
/// </summary>
[CreateAssetMenu(fileName = "PE_", menuName = "Abilities/Passive Effect")]
public class PassiveEffect : Passive, IApplyStatusOverTime, ISubscribeToStateChanged
{
    [SerializeField] private int durationInTurns;
    [SerializeField] private bool applyOnEnterState;
    [SerializeField] private bool applyOnExitState;
    
    public IState State { get; private set; }
    
    public int DurationInTurns
    {
        get => durationInTurns;
        set => durationInTurns = value;
    }

    public Character Character { get; set; }
    public MonoBehaviour Caller { get; set; }
    public void SetState(Character character)
    {
        State = character.GetState();
    }

    public Task Tick()
    {
        return null;
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
    
    private async Task ActivateEffect()
    {
        if (Character == null || Caller == null)
        {
            throw new Exception();
        }
        ((IApplyStatusOverTime)this).TickStatus();
    }
}