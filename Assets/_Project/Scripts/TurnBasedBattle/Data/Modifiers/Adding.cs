using UnityEngine;

[CreateAssetMenu(fileName = "MOD_", menuName = "Modifiers/AddCurrentValue")]
public class Adding : Algorithm
{
    public override void Modify(Statistic stats, Modifier modifier, CharacterFacade user)
    {
        ChangeValues(stats, modifier, user);
    }

    public override void UnModify(Statistic stats, Modifier modifier, CharacterFacade user)
    {
        RemoveChangedValues(stats, modifier, user);
    }


    protected virtual Result ChangeValues(Statistic targetStatsToModify, Modifier userModifier, CharacterFacade user)
    {
        ReportBeforeChange(targetStatsToModify);
        var result = targetStatsToModify.Add(userModifier.GetAmount(user));
        ReportChange(targetStatsToModify, userModifier, user, result);
        return result;
    }

    protected virtual Result RemoveChangedValues(Statistic targetStatsToModify, Modifier userModifier,
        CharacterFacade user)
    {
        ReportBeforeChange(targetStatsToModify);
        var result = targetStatsToModify.Subtract(userModifier.GetAmount(user));
        ReportChange(targetStatsToModify, userModifier, user, result);
        return result;
    }
}