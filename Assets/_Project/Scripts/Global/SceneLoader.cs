using System;
using System.Collections;
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
        if (SceneManager.sceneCount == 1)
        {
            LoadCameras();
            LoadMainMenu();
        }
    }

    public void LoadBattleScenes()
    {
        UnloadMainMenu();
        
        SceneManager.LoadScene(_settings.battle.sceneName, LoadSceneMode.Additive);
        SceneManager.LoadScene(_settings.hud.sceneName, LoadSceneMode.Additive);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(_settings.terrain.sceneName, LoadSceneMode.Additive);
        SceneManager.LoadScene(_settings.cameras.sceneName, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(_settings.mainMenu.sceneName, LoadSceneMode.Additive);
    }

    private void UnloadMainMenu()
    {
        SceneManager.UnloadSceneAsync(_settings.mainMenu.sceneName);
    }
    
    public void LoadLevelWithIndex(int index)
    {
        if (SceneManager.GetActiveScene().name == _settings.Levels[index].sceneName)
        {
            SceneManager.UnloadSceneAsync(_settings.Levels[index].sceneName);
        }

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

    public void LoadCameras()
    {
        if (!SceneManager.GetSceneByName(_settings.cameras.sceneName).isSubScene)
        {
            SceneManager.LoadScene(_settings.cameras.sceneName, LoadSceneMode.Additive);
        }
    }

    public void SetActiveScene(Scene scene)
    {
        SceneManager.SetActiveScene(scene);
    }

    [Serializable]
    public class Settings
    {
        public List<GameScene> Levels = new();

        public GameScene mainMenu;
        public GameScene battle;
        public GameScene hud;
        public GameScene cameras;
        public GameScene terrain;
        public int currentLevelIndex = 0;
        
    }
}