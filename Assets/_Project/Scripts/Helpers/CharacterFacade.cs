using UnityEngine;

public class CharacterFacade : MonoBehaviour
{
    [SerializeField] private Character character;

    private void Start()
    {
        character.InitializeStats(this);
        var stat = character.battleStats.statistics[0];
        var stat2 = character.battleStats.statistics[1];
        var value = stat==stat2;
        Debug.Log(value);

        foreach (var statistic in character.battleStats.statistics)
        {
            Debug.Log(statistic.baseStatistic.statName + " " + statistic.CurrentValue);
        }
    }
}
