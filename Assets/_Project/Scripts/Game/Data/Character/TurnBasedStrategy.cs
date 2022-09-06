using System.Collections.Generic;
using UnityEngine;


public abstract class TurnBasedStrategy : ScriptableObject
{
    public abstract Result SelectTactic(CurrentFightState currentFightState, out SelectedStrategy selectedStrategy);


    public struct CurrentFightState
    {
        public CharacterFacade Character;
        public CharactersLibrary Library;
        public int Points;
        public TurnBasedInputManager TurnBasedInputManager { get; set; }
    }

    public struct SelectedStrategy
    {
        public SelectedStrategy(CharacterFacade character, Active selectedSkill, List<CharacterFacade> selectTargets)
        {
            this.character = character;
            this.selectedSkill = selectedSkill;
            this.selectTargets = selectTargets;
            selected = true;
        }
        public bool selected;
        public CharacterFacade character;
        public Active selectedSkill;
        public List<CharacterFacade> selectTargets;
    }

    
        
    



}