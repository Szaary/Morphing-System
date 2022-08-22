using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterFactory : MonoBehaviour
{
    private SpawnZone _enemySpawnZone;
    private SpawnZone _playerSpawnZone;

    private CharacterFacade.Factory _characterFactory;

    public List<CharacterFacade> spawnedCharacters;
    
    [SerializeField] private Character player;
    [SerializeField] private Character enemy;
    
    [Inject]
    public void Construct(CharacterFacade.Factory characterFactory)
    {
        _characterFactory = characterFactory;
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
    }

    private void SpawnCharacter(Character character)
    {
        var instance = _characterFactory.Create();
        instance.transform.parent= transform;
        instance.gameObject.name= character.name;
        instance.SetCharacter(character);
        SetSpawnZone(instance, character);
        
        spawnedCharacters.Add(instance);
    }

    private void DeSpawnCharacter(CharacterFacade facade)
    {
        spawnedCharacters.Remove(facade);
        Destroy(facade);
    }

    private void SetSpawnZone(CharacterFacade facade, Character character)
    {
        if (character.alignment.alignment == 0)
        {
            _playerSpawnZone.PlaceCharacter(facade);
        }
        else
        {
            _enemySpawnZone.PlaceCharacter(facade);
        }
    }
}