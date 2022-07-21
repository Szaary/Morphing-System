using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AA_", menuName = "Character/Active Ability")]
public class ActiveAbility : ScriptableObject
{
    public bool Use(Character user, List<Character> targets)
    {
        if (user == null) return false;

        foreach (var target in targets)
        {
            Debug.Log("Using ability on: " + target.currentStats.characterName);
        }
       
        return true;
    }
}