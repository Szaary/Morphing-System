using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UILevelManager : MonoBehaviour
{
    [SerializeField] private Settings settings;
    private SceneLoader _sceneLoader;
    private GameManager _gameManager;

    [Inject]
    public void Construct(SceneLoader sceneLoader, GameManager gameManager)
    {
        _sceneLoader = sceneLoader;
        _gameManager = gameManager;
    }

    void Start()
    {
        settings.newGame.onClick.AddListener(NewGame);
    }

    private void OnDestroy()
    {
        settings.newGame.onClick.RemoveListener(NewGame);
    }

    private void NewGame()
    {
        _gameManager.SetGameMode(GameMode.TurnBasedFight);
        _sceneLoader.LoadGameState();
    }


    [Serializable]
    public class Settings
    {
        [Header("Buttons")]
        public Button newGame;
    }
}
