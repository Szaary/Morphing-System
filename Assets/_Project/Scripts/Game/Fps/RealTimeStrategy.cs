using UnityEngine;
using UnityEngine.AI;

public abstract class RealTimeStrategy : ScriptableObject
{
    public abstract Result OnEnter(CurrentFightState currentFightState);
    public abstract Result OnExit(CurrentFightState currentFightState);
    public abstract Result Tick(CurrentFightState currentFightState);
    
    public struct CurrentFightState
    {
        public CharacterFacade Character;
        public CharactersLibrary Library;
        public NavMeshAgent Agent;
    }
}