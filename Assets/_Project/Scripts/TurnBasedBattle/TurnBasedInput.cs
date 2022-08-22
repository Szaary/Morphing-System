using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TurnBasedInput : MonoBehaviour
{
    private TurnStateMachine _stateMachine;

    [Inject]
    public void Construct(TurnStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public void OnUse()
    {
        Debug.Log("OnUse");
    }

    public void OnManageTurn()
    {
        Debug.Log("OnManageTurn");
        _stateMachine.SetState(TurnStateMachine.TurnState.AiTurn);
    }

    public void OnTop()
    {
        Debug.Log("OnTop");
    }

    public void OnDown()
    {
        Debug.Log("OnDown");
    }
    public void OnLeft()
    {
        Debug.Log("OnDown");
    }
    public void OnRight()
    {
        Debug.Log("OnDown");
    }
}
