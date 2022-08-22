using UnityEngine;

/// <summary>
/// Interface you need to implement on caller to manipulate character stats.
/// </summary>
public interface IOperateStats
{
    public CharacterStatistics UserStatistics { get; }
    public MonoBehaviour User { get; }
}