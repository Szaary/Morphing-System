using UnityEngine;

public interface IOperateStats
{
    public CharacterStatistics CharacterStatistics { get; }
    public MonoBehaviour Caller { get; }
}