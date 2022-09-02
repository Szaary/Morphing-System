using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public event Action<GameMode> GameModeChanged;

    private GameMode _lastMode;
    [SerializeField] private GameMode gameMode;
    public GameMode GameMode => gameMode;

    private CharactersLibrary _library;
    private SceneLoader _sceneLoader;

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
        _lastMode = gameMode;
        Debug.Log("Game Mode Changed to: " + newMode);
        gameMode = newMode;
        GameModeChanged?.Invoke(gameMode);
    }

    public void SetPreviousGameMode()
    {
        SetGameMode(_lastMode);
    }

}
