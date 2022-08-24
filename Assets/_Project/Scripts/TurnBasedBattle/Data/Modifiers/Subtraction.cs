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
        ReportBeforeChange(targetStatsToModify);
        var result = targetStatsToModify.Subtract(userModifier.GetAmount(user));
        ReportChange(targetStatsToModify, userModifier, user, result);
    }




    protected virtual void RemoveChangedValues(Statistic targetStatsToModify, Modifier userModifier,
        CharacterFacade user)
    {
        ReportBeforeChange(targetStatsToModify);
        var result = targetStatsToModify.Add(userModifier.GetAmount(user));
        ReportChange(targetStatsToModify, userModifier, user, result);
    }
}