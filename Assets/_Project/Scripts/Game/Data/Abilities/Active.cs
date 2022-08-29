using System.Collections.Generic;
using UnityEngine;

public abstract class Active : ScriptableObject
{
    [Header("VFX")]
    public new string name;
    public Sprite icon;

    public TargetType targetType;
    [Header("Amount of action points need to use action")]
    public int actions;

    public float distance;
    
    public int Position { get; set; }
    
    public abstract int ActivateEffect(CharacterFacade target, CharacterFacade user);

    public bool IsRanged()
    {
        return distance > 1.5f;
    }
    
    public bool IsAttack()
    {
        return targetType is TargetType.Enemies or TargetType.Enemy or TargetType.All;
    }
    public bool IsDefensive()
    {
        return targetType is TargetType.Self or TargetType.Ally or TargetType.Allies or TargetType.All ;
    }
    public bool IsMultiTarget()
    {
        if (targetType is TargetType.Enemies or TargetType.All or TargetType.Allies) return true;
        return false;
    }
    
    public enum TargetType
    {
        Enemy,
        Enemies,
        Self,
        Ally,
        Allies,
        All
    }
}