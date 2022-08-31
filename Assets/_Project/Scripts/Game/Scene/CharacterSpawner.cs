using System;
using UnityEngine;
using Zenject;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Character player;
    [SerializeField] private TurnBasedSpawnZone spawnZone;
    
    private ICharacterFactory _characterFactory;

    [Inject]
    public void Construct(ICharacterFactory characterFactory)
    {
        _characterFactory = characterFactory;
    }

    private void Start()
    {
        _characterFactory.SpawnCharacter(player, spawnZone);
    }
}