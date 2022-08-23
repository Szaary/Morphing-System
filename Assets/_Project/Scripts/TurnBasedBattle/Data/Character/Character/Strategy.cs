using System;
using System.Threading.Tasks;
using UnityEngine;


public abstract class Strategy : ScriptableObject
{
    public abstract Task OnEnter(CurrentFightState currentFightState);
    public abstract Task OnExit(CurrentFightState currentFightState);
    public abstract Task Tick();

    public struct CurrentFightState
    {
        public CharacterFacade Character;
        public int Points;
        public CharactersLibrary Library;
        public Action Reset;
        public ChangeActionPointsDelegate ChangeActionPoints;
    }
}