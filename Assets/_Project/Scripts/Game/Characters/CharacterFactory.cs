using UnityEngine;
using Zenject;

public class CharacterFactory: ICharacterFactory
{
    private readonly CharacterFacade.Factory _characterFactory;

    public CharacterFactory(CharacterFacade.Factory characterFactory)
    {
        _characterFactory = characterFactory;
    }

    public void SpawnCharacter(Character character, BaseSpawnZone turnBasedSpawnZone)
    {
        var facade = _characterFactory.Create(character.prefab, character);
        facade.gameObject.name = character.name;
        SetSpawnZone(facade, turnBasedSpawnZone);
    }

    public void SetSpawnZone(CharacterFacade facade, BaseSpawnZone battleSpawnZone)
    {
        if (facade.Alignment.Id == 0) battleSpawnZone.PlaceCharacter(facade);
        else battleSpawnZone.PlaceCharacter(facade);
    }
}

public interface ICharacterFactory
{
    void SpawnCharacter(Character character, BaseSpawnZone turnBasedSpawnZone);
}