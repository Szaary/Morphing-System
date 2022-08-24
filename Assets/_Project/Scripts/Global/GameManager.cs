using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : IInitializable
{
    private readonly SceneLoader _sceneLoader;

    public void Initialize()
    {
        
    }

    public GameManager(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    
    [Serializable]
    public class Settings
    {
        public int level;
    }
}