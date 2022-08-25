using UnityEngine;
using Zenject;

public class TerrainInitializer : MonoBehaviour
{
    private SceneLoader _sceneLoader;
  
    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void Start()
    {
        _sceneLoader.SetActiveScene(gameObject.scene);
    }
}