using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private Character player;
    [SerializeField] private Character enemy;

    public int PlayerCharacters { get; private set; }
    public int AiCharacters { get; private set; }


    private SpawnZone _enemySpawnZone;
    private SpawnZone _playerSpawnZone;
    private CharacterFacade.Factory _characterFactory;
    private readonly List<CharacterFacade> _spawnedCharacters = new();
    private BattleStart _battleStart;

    [Inject]
    public void Construct(CharacterFacade.Factory characterFactory, BattleStart battleStart)
    {
        _battleStart = battleStart;
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

        _battleStart.AreUnitsSpawned = true;
    }

    private void SpawnCharacter(Character character)
    {
        var facade = _characterFactory.Create();
        facade.transform.parent = transform;
        facade.gameObject.name = character.name;
        facade.SetCharacter(character);
        SetSpawnZone(facade, character);


        facade.DeSpawned += DeSpawnCharacter;

        AddCharacter(facade);
    }

    private void DeSpawnCharacter(CharacterFacade facade)
    {
        facade.DeSpawned -= DeSpawnCharacter;
        RemoveCharacter(facade);
        Debug.Log("Destroying character: " + facade.name);
        Destroy(facade.gameObject);
    }

    private void AddCharacter(CharacterFacade facade)
    {
        _spawnedCharacters.Add(facade);
        CountCharacters();
    }

    private void RemoveCharacter(CharacterFacade facade)
    {
        _spawnedCharacters.Remove(facade);
        CountCharacters();
    }

    private void CountCharacters()
    {
        PlayerCharacters = _spawnedCharacters.Where(x => x.Alignment.id == 0).ToList().Count;
        AiCharacters = _spawnedCharacters.Where(x => x.Alignment.id != 0).ToList().Count;
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