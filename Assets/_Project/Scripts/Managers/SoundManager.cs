using System;
using UnityEngine;
using Zenject;

public class SoundManager : IInitializable
{
    private Settings _settings;

    [Inject]
    public void Construct(Settings settings)
    {
        _settings = settings;
    }
    
    public void Initialize()
    {
        Debug.Log("SoundManager Initialized with volume: "+ _settings.soundVolume);
    }
    
    
    [Serializable]
    public class Settings
    {
        public float soundVolume;
    }
}