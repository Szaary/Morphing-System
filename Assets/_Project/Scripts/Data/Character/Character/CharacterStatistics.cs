using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "CS_", menuName = "Character/Statistics")]
public class CharacterStatistics : ScriptableObject
{
    public enum Result
    {
        Success,
        Failed
    }
    
    public List<Statistic> statistics= new List<Statistic>();

    public void Initialize(CharacterStatistics characterStatistics)
    {
        foreach (var stat in characterStatistics.statistics)
        {
            var instance = CreateInstance<Statistic>();
            instance.Initialize(stat);
            statistics.Add(instance);
        }
    }

    public void Destroy()
    {
        foreach (var stat in statistics)
        {
            if (stat != null)
            {
                Destroy(stat);
            }
        }
    }

    public Result GetStatistic(BaseStatistic baseStatistic, out Statistic outStat)
    {
        foreach (var stat in statistics)
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