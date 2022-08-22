using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive : ScriptableObject, IApplyPersistentStatus
{
    [Header("VFX")]
    public new string name;
    public Sprite icon;
    
    
    [Header("Statistics")]
    [SerializeField] protected List<Modifier> modifiers;


    public List<Modifier> Modifiers
    {
        get => modifiers;
        set => modifiers = value;
    }
}
