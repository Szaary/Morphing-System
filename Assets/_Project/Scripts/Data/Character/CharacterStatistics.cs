using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CS_", menuName = "Character/CharacterStatistics")]
public class CharacterStatistics : ScriptableObject
{
    public string characterName;

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
}