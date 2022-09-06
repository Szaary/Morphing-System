using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AIS_", menuName = "Strategy/HealAiStrategy")]
public class AiHealStrategy : BaseAiTurnBasedStrategy
{
    [SerializeField] private BaseStatistic health;

    public override Result SelectTactic(CurrentFightState currentFightState, out SelectedStrategy selectedStrategy)
    {
        Debug.Log("Selecting Ai strategy by: " + currentFightState.Character.name);
        var result = TacticsLibrary.GetPossibleActions(currentFightState, out var active, out var targets);
        if (result == TacticsLibrary.Possible.NoSuitableSkillsToUse)
        {
            selectedStrategy = new SelectedStrategy();
            return Result.NoSuitableSkillsToUse;
        }

        if (result == TacticsLibrary.Possible.Both)
        {
            var defensive = active.Where(x => x.IsDefensive()).ToList();
            var selectedSkillNumber = Random.Range(0, defensive.Count);
            var selectedSkill = defensive[selectedSkillNumber];
            var selectTargets =
                TacticsLibrary.SelectTargetsBasedOnActive(selectedSkill, targets,
                    currentFightState.Character.Alignment);

            var hurt = new List<CharacterFacade>();
            foreach (var target in targets)
            {
                target.stats.GetStatistic(health, out var stat);
                if (stat.CurrentValue < stat.maxValue)
                {
                    hurt.Add(target);
                    Debug.Log(
                        "Found hurt ally: " + target + "with health: " + stat.CurrentValue + " / " + stat.maxValue);
                }
            }


            if (hurt.Count > 0)
            {
                Debug.Log("Hurt targets count: " + hurt.Count);
                if (!selectedSkill.IsMultiTarget())
                {
                    var randomTarget = Random.Range(0, hurt.Count);
                    var list = new List<CharacterFacade> {hurt[randomTarget]};
                    selectedStrategy = new SelectedStrategy(currentFightState.Character, selectedSkill, list);
                    Debug.Log("Randomly selected target to heal: " + selectTargets[randomTarget]);
                }
                else
                {
                    Debug.Log("Heal everyone");
                    selectedStrategy = new SelectedStrategy(currentFightState.Character, selectedSkill, selectTargets);
                }
                return Result.Success;
            }
            else
            {
                Debug.Log("There were no hurt ally characters, attacking.");
                var offensive = active.Where(x => x.IsAttack()).ToList();
                return SelectRandomSkillForTarget(currentFightState, offensive, targets, out selectedStrategy);
            }
        }

        return SelectRandomSkillForTarget(currentFightState, active, targets, out selectedStrategy);
    }
}