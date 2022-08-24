using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MOD_", menuName = "Modifiers/AddOverTime")]
public class AddingOverTime : Adding
{
    public float timeBetweenUpdates;
    public float timeToEnd;
  
    protected override Result ChangeValues(Statistic targetStatsToModify, Modifier userModifier, CharacterFacade user)
    {
        user.StartCoroutine(AddValuesOverTime(targetStatsToModify, userModifier, user));

        return Result.Success;
    }

    private IEnumerator AddValuesOverTime(Statistic stats, Modifier modifier, CharacterFacade user)
    {
        Debug.LogError("Not done yet");
        base.ChangeValues(stats, modifier, user);
        yield return new WaitForSeconds(timeBetweenUpdates);
    }
}
