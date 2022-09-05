using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameModeChangeCooldown;
    [SerializeField] private GameMode gameMode;
    
    public event Action<GameMode> GameModeChanged;
    public GameMode GameMode => gameMode;
    
    
    private GameMode _lastMode;
    private CharactersLibrary _library;
    private SceneLoader _sceneLoader;
    private bool isOnCooldown;

    [Inject]
    public void Construct(CharactersLibrary library, SceneLoader sceneLoader)
    {
        _library = library;
        _sceneLoader = sceneLoader;
    }

    private void Start()
    {
        _library.noAllies += OnNoAllies;
        _library.noEnemies += OnNoEnemies;
    }

    private void OnNoEnemies()
    {
        if(gameMode== GameMode.TurnBasedFight) SetPreviousGameMode();
    }

    private void OnNoAllies()
    {
        _sceneLoader.RestartGame();
    }

    private void OnDestroy()
    {
        _library.noAllies -= OnNoAllies;
        _library.noEnemies -= OnNoEnemies;
    }

    public void SetGameMode(GameMode newMode)
    {
        if (gameMode == newMode) return;
        if (isOnCooldown) return;
        _lastMode = gameMode;
        Debug.Log("Game Mode Changed to: " + newMode);
        gameMode = newMode;
        GameModeChanged?.Invoke(gameMode);
        isOnCooldown = true;
        Invoke(nameof(Reset), gameModeChangeCooldown);
    }
    
    public void Reset()
    {
        isOnCooldown = false;
    }


    public void SetPreviousGameMode()
    {
        SetGameMode(_lastMode);
    }

}
