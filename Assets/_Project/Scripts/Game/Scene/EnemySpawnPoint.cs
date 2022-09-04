using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider))]
public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private TurnBasedSpawnZone battleSpawnZone;
    [SerializeField] private List<Character> charactersToSpawn;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private bool randomizeEnemies;

    [SerializeField] private bool forceGameMode;
    [SerializeField] private GameMode gameMode;

    [SerializeField] private bool isSpawned;
    private CharactersLibrary _library;
    private ICharacterFactory _characterFactory;
    private GameManager _gameManager;

    [Inject]
    public void Construct(ICharacterFactory characterFactory, CharactersLibrary library, GameManager gameManager)
    {
        _characterFactory = characterFactory;
        _library = library;
        _gameManager = gameManager;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isSpawned) return;
        if (_library.SpawnedAllCharacters) return;


        if (other.TryGetComponent(out CharacterFacade facade))
        {
            if (facade.Alignment.ID != 0) return;

            if (forceGameMode) _gameManager.SetGameMode(gameMode);

            int numberOfEnemies = charactersToSpawn.Count;
            if (randomizeEnemies)
            {
                numberOfEnemies = Random.Range(0, 3);
            }

            for (var index = 0; index < numberOfEnemies; index++)
            {
                var character = charactersToSpawn[index];
                _characterFactory.SpawnCharacter(character, battleSpawnZone);
            }

            var playerPosition = battleSpawnZone.GetSpawnPosition(facade.stats.character);
            facade.SetBattlePosition(playerPosition);
            
            _library.SpawnedAllCharacters = true;
            isSpawned = true;
        }
    }


    private void OnValidate()
    {
        if (charactersToSpawn.Count > 4) Debug.LogError("Only 4 enemies possible");
        if (gameMode == GameMode.InMenu && forceGameMode) Debug.LogError("You need to select game mode");
    }
    
    private void OnDrawGizmos()
    {
        if (boxCollider == null) return;
        GizmosExtensions.DrawBoxCollider(Color.yellow, boxCollider, 0.1f, transform);
    }
}