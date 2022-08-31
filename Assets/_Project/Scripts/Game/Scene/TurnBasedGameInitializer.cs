using UnityEngine;
using Zenject;

public class TurnBasedGameInitializer : MonoBehaviour
{
    [SerializeField] private TurnBasedSpawnZone battleSpawnZone; 
    [SerializeField] private Character player;
    [SerializeField] private Character enemy;

    
    private CharactersLibrary _library;
    private ICharacterFactory _characterFactory;

    [Inject]
    public void Construct(ICharacterFactory characterFactory, CharactersLibrary library)
    {
        _characterFactory = characterFactory;
        _library = library;
    }

    private void Start()
    {
        SpawnInitialCharacters(battleSpawnZone);
    }
    public void SpawnInitialCharacters(BaseSpawnZone turnBasedSpawnZone)
    {
        _characterFactory.SpawnCharacter(player, turnBasedSpawnZone);
        
        _characterFactory.SpawnCharacter(enemy, turnBasedSpawnZone);
        _characterFactory.SpawnCharacter(enemy, turnBasedSpawnZone);
        _characterFactory.SpawnCharacter(enemy, turnBasedSpawnZone);
        _characterFactory.SpawnCharacter(enemy, turnBasedSpawnZone);
        
        _library.SpawnedAllCharacters = true;
    }

}