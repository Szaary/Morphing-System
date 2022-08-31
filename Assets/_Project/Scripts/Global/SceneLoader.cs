using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : IInitializable
{
    private readonly Settings _settings;
    private int _levelIndex;
    
    public SceneLoader(Settings settings)
    {
        _settings = settings;
    }

    public void Initialize()
    {
        InitializeGame();
    }
    
    private void InitializeGame()
    {
        SceneManager.LoadSceneAsync(_settings.terrain.sceneName, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(_settings.mainMenu.sceneName, LoadSceneMode.Additive);
        
        SceneManager.LoadScene(_settings.cameras.sceneName, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(_settings.characters.sceneName, LoadSceneMode.Additive);
    }

    
    
    public void LoadGameState()
    {
        Debug.Log("Unloading Main Menu");
        if (SceneManager.GetSceneByName(_settings.mainMenu.sceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(_settings.mainMenu.sceneName);
        }
        
        SceneManager.LoadSceneAsync(_settings.hud.sceneName, LoadSceneMode.Additive);

        LoadLevelWithIndex(_levelIndex);
    }
 
    
    public void LoadLevelWithIndex(int index)
    {
        if (index <= _settings.Levels.Count)
        {
            SceneManager.LoadSceneAsync(_settings.Levels[index].sceneName, LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("Scene does not exist");
        }
    }

    [Serializable]
    public class Settings
    {
        public List<GameScene> Levels = new();

        public GameScene mainMenu;
        public GameScene characters;
        public GameScene hud;
        public GameScene cameras;
        public GameScene terrain;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SC_Startup");
    }
}