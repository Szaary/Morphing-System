using UnityEngine;
using Zenject;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private SpawnZone playerSpawnZone; 
    [SerializeField] private SpawnZone enemySpawnZone;
    
    private SceneLoader _sceneLoader;
    private CharacterFactory _characterFactory;

    [Inject]
    public void Construct(SceneLoader sceneLoader, CharacterFactory characterFactory)
    {
        _sceneLoader = sceneLoader;
        _characterFactory = characterFactory;
    }

    private void Start()
    {
        _sceneLoader.SetActiveScene(gameObject.scene);
        _characterFactory.SetSpawnZones(playerSpawnZone, enemySpawnZone);
    }
}
