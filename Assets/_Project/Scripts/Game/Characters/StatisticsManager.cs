using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    [Header("Do not set anything here, will be changed ad app start.")]
    public Character character;

    public void SetCharacter(Character characterTemplate)
    {
        character = characterTemplate.Clone();
        character.CreateInstances();
    }
    
    private void OnDestroy()
    {
        character.RemoveInstances();
        Destroy(character);
    }
    
    public Result GetStatistic(BaseStatistic baseStatistic, out Statistic outStat)
    {
        foreach (var stat in character.statistics)
        {
            if (stat.baseStatistic == baseStatistic)
            {
                outStat = stat;
                return Result.Success;
            }
        }
        outStat = null;
        return Result.Failed;
    }

    
}
