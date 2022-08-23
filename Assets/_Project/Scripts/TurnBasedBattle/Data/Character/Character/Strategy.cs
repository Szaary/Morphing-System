using System;
using System.Threading.Tasks;
using UnityEngine;


public abstract class Strategy : ScriptableObject
{
    public abstract Task OnEnter(TurnController.ChangeActionPointsDelegate removeActionPointsDelegate,
        CurrentFightState currentFightState);
    public abstract Task OnExit(TurnController.ChangeActionPointsDelegate endTurn, CurrentFightState currentFightState);
    public abstract Task Tick(TurnController.ChangeActionPointsDelegate endTurn, CurrentFightState currentFightState);

    public struct CurrentFightState
    {
        public CharacterFacade character;
        public int Points;
        public CharactersLibrary library;
    }
}