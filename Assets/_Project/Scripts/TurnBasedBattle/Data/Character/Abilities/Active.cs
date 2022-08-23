using System.Collections.Generic;
using UnityEngine;

public abstract class Active : ScriptableObject
{
    [Header("VFX")]
    public new string name;
    public Sprite icon;

    public Target target;
    [Header("Amount of action points need to use action")]
    public int actions;

    public abstract int ActivateEffect(Character target, IOperateStats user);
    
    public bool IsAttack(Active active)
    {
        if (target is Target.Enemies or Target.Enemy or Target.All) return true;
        return false;
    }

    public bool IsMultiTarget()
    {
        if (target is Target.Enemies or Target.All or Target.Allies) return true;
        return false;
    }
    
    public enum Target
    {
        Enemy,
        Enemies,
        Self,
        Ally,
        Allies,
        All
    }
}