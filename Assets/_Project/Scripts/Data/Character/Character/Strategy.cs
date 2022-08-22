using System;
using System.Threading.Tasks;
using UnityEngine;


public abstract class Strategy : ScriptableObject
{
    public abstract Task OnEnter(Action endTurn);
    public abstract Task OnExit(Action endTurn);
    public abstract Task Tick(Action endTurn);

}