using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MenuElements : ScriptableObject
{
    [MenuItem("Shortcuts/Edit Terrain")]
    static void Terrain()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/SC_Terrain.unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/SC_Cameras.unity", OpenSceneMode.Additive);
        
    }
    
    [MenuItem("Shortcuts/Edit 2 Battle")]
    static void Battle2D()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/SC_Terrain.unity", OpenSceneMode.Additive);
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/SC_Cameras.unity", OpenSceneMode.Additive);

        EditorSceneManager.OpenScene("Assets/_Project/Scenes/TurnBased/SC_HudTurnBasedBattle.unity", OpenSceneMode.Additive);
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/TurnBased/SC_TurnBasedBattle.unity", OpenSceneMode.Additive);
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/TurnBased/SC_TurnBasedBattleLevel2D.unity", OpenSceneMode.Additive);
    }
    
    
    [MenuItem("Shortcuts/StartGame")]
    static void StartGame()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/SC_Startup.unity", OpenSceneMode.Single);
        EditorApplication.EnterPlaymode();
    }

    
}
