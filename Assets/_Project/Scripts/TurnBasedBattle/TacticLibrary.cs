public static class TacticLibrary
{
    public static void RandomAttack(ChangeActionPointsDelegate removeActionPointsDelegate,
        Strategy.CurrentFightState currentFightState)
    {
        if (currentFightState.Character.Active.GetRandomAttack(currentFightState.Points, out Active skill) ==
            ActiveManager.Result.Success)
        {
            if (skill.IsMultiTarget())
            {
                var targets = currentFightState.Library.SelectAllEnemies(currentFightState.Character.Alignment);
                foreach (var target in targets)
                {
                    skill.ActivateEffect(target.GetCharacter(), currentFightState.Character.GetCharacter());
                }
            }
            else
            {
                var target = currentFightState.Library.SelectRandomEnemy(currentFightState.Character.Alignment);
                skill.ActivateEffect(target.GetCharacter(), currentFightState.Character.GetCharacter());
            }

            removeActionPointsDelegate(skill.actions);
        }
        else
        {
            removeActionPointsDelegate();
        }
    }
}