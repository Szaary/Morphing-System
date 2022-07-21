using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly Settings _settings;

    public SceneLoader(Settings settings)
    {
        _settings = settings;
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
        
        if (index <= _settings.Levels.Count)
        {
            SceneManager.LoadSceneAsync(_settings.Levels[index].sceneName);
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
        public int currentLevelIndex = 0;
    }
}