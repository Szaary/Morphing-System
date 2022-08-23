public static class TacticLibrary
{
    public static void RandomAttack(TurnController.ChangeActionPointsDelegate removeActionPointsDelegate,
        Strategy.CurrentFightState currentFightState)
    {
        if (currentFightState.character.Active.GetRandomAttack(currentFightState.Points, out Active skill) ==
            ActiveManager.Result.Success)
        {
            if (skill.IsMultiTarget())
            {
                var targets = currentFightState.library.SelectAllEnemies(currentFightState.character.Alignment);
                foreach (var target in targets)
                {
                    skill.ActivateEffect(target.GetCharacter(), currentFightState.character.GetCharacter());
                }
            }
            else
            {
                var target = currentFightState.library.SelectRandomEnemy(currentFightState.character.Alignment);
                skill.ActivateEffect(target.GetCharacter(), currentFightState.character.GetCharacter());
            }

            removeActionPointsDelegate(skill.actions);
        }
        else
        {
            removeActionPointsDelegate();
        }
    }
}