using UnityEngine;
using Zenject;

public class TurnBasedGameInitializer : MonoBehaviour
{
    [SerializeField] private SpawnZone playerSpawnZone; 
    [SerializeField] private SpawnZone enemySpawnZone;
    
    private CharacterFactory _characterFactory;

    [Inject]
    public void Construct(CharacterFactory characterFactory)
    {
        _characterFactory = characterFactory;
    }

    private void Start()
    {
        _characterFactory.SetSpawnZones(playerSpawnZone, enemySpawnZone);
    }
}