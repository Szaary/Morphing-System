using UnityEngine;

[CreateAssetMenu(fileName = "MOD_", menuName = "Modifiers/Subtract")]
public class Subtraction : Algorithm
{
    public override void Modify(Statistic stats, Modifier modifier, CharacterFacade user)
    {
        ChangeValues(stats, modifier, user);
    }

    public override void UnModify(Statistic stats, Modifier modifier, CharacterFacade user)
    {
        RemoveChangedValues(stats, modifier, user);
    }

    protected virtual void ChangeValues(Statistic targetStatsToModify, Modifier userModifier, CharacterFacade user)
    {
        var result = targetStatsToModify.Subtract(userModifier.GetAmount(user));
        Debug.Log("Changed " + userModifier.statisticToModify.statName + " by " + userModifier.GetAmount(user) +
                  " stat: " + targetStatsToModify.baseStatistic.statName + " by " + user +
                  ". Current value is: " + targetStatsToModify.CurrentValue + " with result: " + result);
    }

    protected virtual void RemoveChangedValues(Statistic targetStatsToModify, Modifier userModifier,
        CharacterFacade user)
    {
        var result = targetStatsToModify.Add(userModifier.GetAmount(user));
        Debug.Log("Changed " + userModifier.statisticToModify.statName + " by " + userModifier.GetAmount(user) +
                  " stat: " + targetStatsToModify.baseStatistic.statName + " by " + user +
                  ". Current value is: " + targetStatsToModify.CurrentValue + " with result: " + result);
    }
}