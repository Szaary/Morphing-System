using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainInitializer : MonoBehaviour
{
    private void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
    }
}