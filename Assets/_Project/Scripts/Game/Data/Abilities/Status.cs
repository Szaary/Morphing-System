using System.Collections.Generic;
using UnityEngine;

public abstract class Status : ScriptableObject
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
    
    public virtual Result ApplyStatus(CharacterFacade target, CharacterFacade user)
    {
        return target.Modify(user, Modifiers);
    }
    
    public virtual Result RemoveStatus(CharacterFacade target, CharacterFacade user)
    {
        return target.UnModify(user, Modifiers);;
    }
}
