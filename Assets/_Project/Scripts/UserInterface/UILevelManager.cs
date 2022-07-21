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

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
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
        _sceneLoader.NewGame();
    }


    [Serializable]
    public class Settings
    {
        [Header("Buttons")]
        public Button newGame;
    }
}
