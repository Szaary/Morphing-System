using UnityEngine;


public abstract class TurnBasedStrategy : ScriptableObject
{
    public abstract Result OnEnter(CurrentFightState currentFightState);
    public abstract Result OnExit(CurrentFightState currentFightState);
    public abstract Result Tick();

    public struct CurrentFightState
    {
        public CharacterFacade Character;
        public CharactersLibrary Library;
        
        public int Points;
        public ChangeActionPointsDelegate ChangeActionPoints;
        public TurnBasedInput Inputs;
    }
}