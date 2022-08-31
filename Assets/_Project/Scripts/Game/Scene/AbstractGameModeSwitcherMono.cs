using UnityEngine;
using Zenject;

public abstract class AbstractGameModeSwitcherMono : MonoBehaviour
{
    protected GameManager GameManager;

    [Inject]
    public void Construct(GameManager manager)
    {
        GameManager = manager;
    }

    protected virtual void Start()
    {
        SubscribeToGameModeChange();
    }

    private void SubscribeToGameModeChange()
    {
        OnGameModeChanged(GameManager.GameMode);
        GameManager.GameModeChanged += OnGameModeChanged;
    }

    protected abstract void OnGameModeChanged(GameMode newMode);

    protected virtual void OnDestroy()
    {
        GameManager.GameModeChanged -= OnGameModeChanged;
    }
}