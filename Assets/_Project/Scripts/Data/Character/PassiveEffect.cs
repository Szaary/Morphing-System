using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PE_", menuName = "Character/Passive Effect")]
public class PassiveEffect : ScriptableObject, IModifyStats
{
    public bool OnApplyStatus(Character character)
    {
        if (character == null) return false;
        Debug.Log("Character health is: "+ character.name);

        return true;
    }

    public bool OnRemoveStatus(Character character)
    {
        return true;
    }
}