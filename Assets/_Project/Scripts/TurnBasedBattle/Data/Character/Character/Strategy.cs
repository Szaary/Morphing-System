using System;
using System.Threading.Tasks;
using UnityEngine;


public abstract class Strategy : ScriptableObject
{
    public abstract Task OnEnter(ChangeActionPointsDelegate removeActionPointsDelegate,
        CurrentFightState currentFightState);
    public abstract Task OnExit(ChangeActionPointsDelegate endTurn, CurrentFightState currentFightState);
    public abstract Task Tick(ChangeActionPointsDelegate endTurn, CurrentFightState currentFightState);

    public struct CurrentFightState
    {
        public CharacterFacade Character;
        public int Points;
        public CharactersLibrary Library;
    }
}