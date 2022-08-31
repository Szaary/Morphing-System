using UnityEngine;
using Zenject;

public abstract class GameModeSwitcherMono : MonoBehaviour
{
    private GameManager gameManager;

    [Inject]
    public void Construct(GameManager manager)
    {
        gameManager = manager;
    }

    protected virtual void Start()
    {
        SubscribeToGameModeChange();
    }

    private void SubscribeToGameModeChange()
    {
        OnGameModeChanged(gameManager.GameMode);
        gameManager.GameModeChanged += OnGameModeChanged;
    }

    protected abstract void OnGameModeChanged(GameMode newMode);

    protected virtual void OnDestroy()
    {
        gameManager.GameModeChanged -= OnGameModeChanged;
    }
}