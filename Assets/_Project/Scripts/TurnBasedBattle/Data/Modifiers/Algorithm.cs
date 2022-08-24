using UnityEngine;

public abstract class Algorithm : ScriptableObject 
{
    public abstract void Modify(Statistic stats, Modifier modifier, CharacterFacade user);
    public abstract void UnModify(Statistic stats, Modifier modifier, CharacterFacade user);
    
    protected static void ReportChange(Statistic targetStatsToModify, Modifier userModifier, CharacterFacade user,
        Result result)
    {
        Debug.Log("Changed " + userModifier.statisticToModify.statName + " by " + userModifier.GetAmount(user) +
                  " stat: " + targetStatsToModify.baseStatistic.statName + " by " + user +
                  ". Current value is: " + targetStatsToModify.CurrentValue + " with result: " + result);
    }
    
    protected static void ReportBeforeChange(Statistic targetStatsToModify)
    {
        Debug.Log("Statistic before modification: " + targetStatsToModify + targetStatsToModify.CurrentValue);
    }

}
