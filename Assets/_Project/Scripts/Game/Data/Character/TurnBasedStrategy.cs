using System.Collections.Generic;
using UnityEngine;


public abstract class TurnBasedStrategy : ScriptableObject
{
    public abstract Result SelectTactic(CurrentFightState currentFightState);


    public struct CurrentFightState
    {
        public CharacterFacade Character;
        public CharactersLibrary Library;
        public int Points;
        public TurnBasedInputManager TurnBasedInputManager { get; set; }
    }
}