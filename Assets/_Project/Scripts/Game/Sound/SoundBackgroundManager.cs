using System;
using FMODUnity;
using UnityEngine;
using Zenject;

public class SoundBackgroundManager : MonoBehaviour
{
    [SerializeField] private Settings settings;
    private GameManager _gameManager;
    private SoundManager _soundManager;

    [Inject]
    public void Construct(GameManager gameManager, SoundManager soundManager)
    {
        _gameManager = gameManager;
        _soundManager = soundManager;
    }

    private void Start()
    {
        OnGameModeChanged(_gameManager.GameMode);
        _gameManager.GameModeChanged += OnGameModeChanged;
    }

    private void OnGameModeChanged(GameMode newMode)
    {
        switch (newMode)
        {
            case GameMode.InMenu:
                _soundManager.PlayLevelSounds(settings.ambient, settings.menuMusic);
                break;
            case GameMode.Adventure:
                _soundManager.PlayMusicWithFadeOut(settings.adventure);
                break;
            case GameMode.TurnBasedFight:
                _soundManager.PlayMusicWithFadeOut(settings.jRPG);
                break;
            case GameMode.Fps:
                _soundManager.PlayMusicWithFadeOut(settings.fps);
                break;
            case GameMode.Tpp:
                _soundManager.PlayMusicWithFadeOut(settings.disco);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newMode), newMode, null);
        }
    }
    
    


    [Serializable]
    public class Settings
    {
        public EventReference adventure;
        public EventReference disco;
        public EventReference fpsFight;
        public EventReference fps;
        public EventReference jRPG;
        
        public EventReference ambient;
        public EventReference menuMusic;
    }
}