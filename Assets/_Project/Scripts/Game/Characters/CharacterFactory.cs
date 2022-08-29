using UnityEngine;
using Zenject;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private Character player;
    [SerializeField] private Character enemy;

    private CharacterFacade.Factory _characterFactory;
    private CharactersLibrary _library;

    [Inject]
    public void Construct(CharacterFacade.Factory characterFactory, CharactersLibrary library)
    {
        _characterFactory = characterFactory;
        _library = library;
    }

    public void SpawnInitialCharacters(BaseSpawnZone turnBasedSpawnZone)
    {
        SpawnCharacter(player, turnBasedSpawnZone);
        
        SpawnCharacter(enemy, turnBasedSpawnZone);
        SpawnCharacter(enemy, turnBasedSpawnZone);
        SpawnCharacter(enemy, turnBasedSpawnZone);
        SpawnCharacter(enemy, turnBasedSpawnZone);
        
        _library.SpawnedAllCharacters = true;
    }

    private void SpawnCharacter(Character character, BaseSpawnZone turnBasedSpawnZone)
    {
        var facade = _characterFactory.Create(character);
        facade.transform.parent = transform;
        facade.gameObject.name = character.name;
        
        SetSpawnZone(facade, turnBasedSpawnZone);
    }

    private void SetSpawnZone(CharacterFacade facade, BaseSpawnZone battleSpawnZone)
    {
        if (facade.Alignment.Id == 0) battleSpawnZone.PlaceCharacter(facade);
        else battleSpawnZone.PlaceCharacter(facade);
    }
}