using System;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "CS_", menuName = "Character/Statistics")]
public class CharacterStatistics : ScriptableObject
{
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
}