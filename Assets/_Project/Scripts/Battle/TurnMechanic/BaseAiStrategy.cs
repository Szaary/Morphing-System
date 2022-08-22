using System;
using System.Threading.Tasks;
using UnityEngine;

//CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/BaseAiStrategy")]
public class BaseAiStrategy : Strategy
{
    public override async Task OnEnter(Action endTurn)
    {
        Debug.Log("Before doing action by AI");
        await Task.Delay(1000);
        Debug.Log("After doing action by AI");
        
        endTurn();
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