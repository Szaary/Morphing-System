using System.Collections.Generic;
using System.Linq;
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
        Debug.Log("Spawn zones set");
        SpawnInitialCharacters();
    }

    private void SpawnInitialCharacters()
    {
        SpawnCharacter(player);
        SpawnCharacter(enemy);

        _library.SpawnedAllCharacters = true;
    }

    private void SpawnCharacter(Character character)
    {
        var facade = _characterFactory.Create();
        facade.transform.parent = transform;
        facade.gameObject.name = character.name;
        facade.SetCharacter(character);
        SetSpawnZone(facade, character);


        facade.DeSpawned += DeSpawnCharacter;

        _library.AddCharacter(facade);
    }

    private void DeSpawnCharacter(CharacterFacade facade)
    {
        facade.DeSpawned -= DeSpawnCharacter;
        _library.RemoveCharacter(facade);
        Debug.Log("Destroying character: " + facade.name);
        Destroy(facade.gameObject);
    }

   

    private void SetSpawnZone(CharacterFacade facade, Character character)
    {
        if (character.Alignment.Id == 0)
        {
            _playerSpawnZone.PlaceCharacter(facade);
        }
        else
        {
            _enemySpawnZone.PlaceCharacter(facade);
        }
    }
}