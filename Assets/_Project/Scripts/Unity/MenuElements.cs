using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

#endif
public class MenuElements : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("Shortcuts/Edit Terrain")]
    static void Terrain()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/TemplateLevel/SC_TemplateTerrain.unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/TemplateLevel/SC_TemplateTerrainLogic.unity", OpenSceneMode.Additive);
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/SC_Cameras.unity", OpenSceneMode.Additive);
    }
    
    [MenuItem("Shortcuts/Creators")]
    static void CharacterCreator()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/SC_Creators.unity", OpenSceneMode.Additive);
    }

    [MenuItem("Shortcuts/StartGame")]
    static void StartGame()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/SC_Startup.unity", OpenSceneMode.Single);
        EditorApplication.EnterPlaymode();
    }
#endif
    
}

