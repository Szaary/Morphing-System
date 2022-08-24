/// <summary>
/// Interface you need to implement by persistent effects to apply status effects on target.
/// </summary>
public interface IApplyPersistentStatus : IModifyStats
{
    Result OnRemoveStatus(CharacterFacade target, CharacterFacade user)
    {
        return target.UnModify(user, Modifiers);;
    }
}