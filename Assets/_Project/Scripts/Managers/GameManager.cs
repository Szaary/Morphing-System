using System;
using UnityEngine;
using Zenject;

public class GameManager : IInitializable
{
    public void Initialize()
    {
        Debug.Log("GameManager.Initialize");
    }
    
    [Serializable]
    public class Settings
    {
        public int level;
    }
}