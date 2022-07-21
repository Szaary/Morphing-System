using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : IInitializable
{
    private readonly Settings _settings;

    public SceneLoader(Settings settings)
    {
        _settings = settings;
    }
    
    public void Initialize()
    {
        
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(_settings.mainMenu.sceneName, LoadSceneMode.Additive);
    }
    private void UnloadMainMenu()
    {
        SceneManager.UnloadSceneAsync(_settings.mainMenu.sceneName);
    }
    
    

    public void NewGame()
    {
        LoadLevelWithIndex(0);
    }
    
    
    
    private void LoadLevelWithIndex(int index)
    {
        if (SceneManager.GetActiveScene().name == _settings.Levels[index].sceneName)
        {
            SceneManager.UnloadSceneAsync(_settings.Levels[index].sceneName);
        }

        UnloadMainMenu();

        if (index <= _settings.Levels.Count)
        {
            SceneManager.LoadSceneAsync(_settings.Levels[index].sceneName, LoadSceneMode.Additive);
            _settings.currentLevelIndex = index;
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
        
        public int currentLevelIndex = 0;
    }


}