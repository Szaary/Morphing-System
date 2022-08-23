using UnityEditor;
using UnityEngine;
using Zenject;

public class TurnBasedInput : MonoBehaviour
{
    private TurnStateMachine _stateMachine;
    private TargetSelector _selector;
    public PlayerStrategy playerStrategy;
    
    [Inject]
    public void Construct(TurnStateMachine stateMachine, TargetSelector selector)
    {
        _stateMachine = stateMachine;
        _selector = selector;
    }
    
    public void OnUse()
    {
        if (StopPlayerAction()) return;
        Debug.Log("OnUse");
    }

    public void OnManageTurn()
    {
        Debug.Log("OnManageTurn");
        _stateMachine.SetState(TurnStateMachine.TurnState.AiTurn);
    }

    public void OnTop()
    {
        if (StopPlayerAction()) return;
        if (SetTarget()) return;
        Debug.Log("OnTop");
    }

    public void OnDown()
    {
        if (StopPlayerAction()) return;
        if (SetTarget()) return;
        Debug.Log("OnDown");
    }
    public void OnLeft()
    {
        if (StopPlayerAction()) return;
        if (SetTarget()) return;
        Debug.Log("OnDown");
    }
    public void OnRight()
    {
        if (StopPlayerAction()) return;
        if (SetTarget()) return;
        
        
        Debug.Log("OnDown");
    }

    private bool SetTarget()
    {
        if (_selector.currentlyTargeted == null)
        {
            _selector.Target(TargetSelector.Direction.Top);
            return true;
        }

        return false;
    }

    private bool StopPlayerAction()
    {
        if (_stateMachine.GetCurrentState() != TurnStateMachine.TurnState.PlayerTurn) return true;
        return false;
    }
}
