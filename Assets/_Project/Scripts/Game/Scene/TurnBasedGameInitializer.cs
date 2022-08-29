using UnityEngine;
using Zenject;

public class TurnBasedGameInitializer : MonoBehaviour
{
    [SerializeField] private TurnBasedSpawnZone battleSpawnZone; 
    
    private CharacterFactory _characterFactory;

    [Inject]
    public void Construct(CharacterFactory characterFactory)
    {
        _characterFactory = characterFactory;
    }

    private void Start()
    {
        _characterFactory.SpawnInitialCharacters(battleSpawnZone);
    }
}