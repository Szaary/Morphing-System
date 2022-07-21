using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PA_", menuName = "Character/Passive Ability")]
public class PassiveAbility : ScriptableObject, IModifyStats
{
    public Statistic statisticToModify;
    
    public bool OnApplyStatus(Character character)
    {
        if (character == null) return false;
        Debug.Log("Character health is: "+ character.name);
        return true;
    }

    public bool OnRemoveStatus(Character character)
    {
        if (character == null) return false;
        return true;
    }
}