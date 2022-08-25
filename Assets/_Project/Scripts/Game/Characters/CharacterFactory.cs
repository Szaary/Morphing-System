using UnityEngine;
using Zenject;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private Character player;
    [SerializeField] private Character enemy;
  
    private SpawnZone _enemySpawnZone;
    private SpawnZone _playerSpawnZone;
    
    private CharacterFacade.Factory _characterFactory;
    private CharactersLibrary _library;

    [Inject]
    public void Construct(CharacterFacade.Factory characterFactory, CharactersLibrary library)
    {
        _characterFactory = characterFactory;
        _library = library;
    }

    public void SetSpawnZones(SpawnZone playerSpawnZone, SpawnZone enemySpawnZone)
    {
        _playerSpawnZone = playerSpawnZone;
        _enemySpawnZone = enemySpawnZone;
        SpawnInitialCharacters();
    }

    private void SpawnInitialCharacters()
    {
        SpawnCharacter(player);
        
        SpawnCharacter(enemy);
        SpawnCharacter(enemy);
        SpawnCharacter(enemy);
        SpawnCharacter(enemy);
        
        _library.SpawnedAllCharacters = true;
    }

    private void SpawnCharacter(Character character)
    {
        var facade = _characterFactory.Create(character);
        facade.transform.parent = transform;
        facade.gameObject.name = character.name;
        
        SetSpawnZone(facade);
    }

    private void SetSpawnZone(CharacterFacade facade)
    {
        if (facade.Alignment.Id == 0) _playerSpawnZone.PlaceCharacter(facade);
        else _enemySpawnZone.PlaceCharacter(facade);
    }
}