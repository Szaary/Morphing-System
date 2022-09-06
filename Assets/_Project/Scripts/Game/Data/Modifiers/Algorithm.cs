using UnityEngine;

public abstract class Algorithm : ScriptableObject
{
    [SerializeField] private bool hideLogs = true;
    public abstract void Modify(Statistic stats, Modifier modifier, CharacterFacade user);
    public abstract void UnModify(Statistic stats, Modifier modifier, CharacterFacade user);
    
    protected void ReportChange(Statistic targetStatsToModify, Modifier userModifier, CharacterFacade user,
        Result result)
    {
        if (hideLogs) return;
        Debug.Log("Changed " + userModifier.statisticToModify.statName + " by " + userModifier.GetAmount(user) +
                  " stat: " + targetStatsToModify.baseStatistic.statName + " by " + user +
                  ". Current value is: " + targetStatsToModify.CurrentValue + " with result: " + result);
    }
    
    protected void ReportBeforeChange(Statistic targetStatsToModify)
    {
        if (hideLogs) return;
        Debug.Log("Statistic before modification: " + targetStatsToModify + targetStatsToModify.CurrentValue);
    }

}
