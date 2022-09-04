public class CharacterFactory: ICharacterFactory
{
    private readonly CharacterFacade.Factory _characterFactory;

    public CharacterFactory(CharacterFacade.Factory characterFactory)
    {
        _characterFactory = characterFactory;
    }

    public void SpawnCharacter(Character character, BaseSpawnZone turnBasedSpawnZone)
    {
        var position =  turnBasedSpawnZone.GetSpawnPosition(character);
        var facade = _characterFactory.Create(character.prefab, character);
        
        facade.transform.position = position.transform.position;
        facade.gameObject.name = character.name;
        facade.SetPosition(position);
    }
}

public interface ICharacterFactory
{
    void SpawnCharacter(Character character, BaseSpawnZone turnBasedSpawnZone);
}