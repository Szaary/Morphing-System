using System.Collections.Generic;

/// <summary>
/// Basic interface to modify stats by effects/abilities
/// </summary>
public interface IModifyStats
{
    List<Modifier> Modifiers { get; set; }

    Result OnApplyStatus(CharacterFacade target, CharacterFacade user)
    {
        return target.Modify(user, Modifiers);
    }
}