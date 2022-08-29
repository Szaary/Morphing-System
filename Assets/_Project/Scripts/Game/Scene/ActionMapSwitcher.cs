using System;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class ActionMapSwitcher : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private FpsInput fpsInputs; 
    
    
    private GameManager _gameManager;
    private InputActionMap _turnBasedInput;
    private InputActionMap _fpsInput;
    private InputActionMap _menu;

    private readonly List<InputActionMap> _actionMaps=new();
    
    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Start()
    {
        _turnBasedInput= playerInput.actions.FindActionMap("TurnBasedInput");
        _fpsInput =  playerInput.actions.FindActionMap("FpsInput");
        _menu =  playerInput.actions.FindActionMap("UI");
        
        _actionMaps.Add(_turnBasedInput);
        _actionMaps.Add(_fpsInput);
        _actionMaps.Add(_menu);
        
        OnGameModeChanged(_gameManager.GameMode);
        _gameManager.GameModeChanged += OnGameModeChanged;
    }

    private void OnGameModeChanged(GameMode newMode)
    {
        if (newMode == GameMode.TurnBasedFight)
        {
            SelectActionMap(_turnBasedInput);
            SetCursorState(false);
        }
        else if (newMode== GameMode.Fps)
        {
            SelectActionMap(_fpsInput);
            SetCursorState(true);
        }
        else if (newMode == GameMode.InMenu)
        {
            SelectActionMap(_menu);
            SetCursorState(false);
        }
    }

    private void SelectActionMap(InputActionMap selected)
    {
        foreach (var map in _actionMaps)
        {
            if (map == selected)
            {
                map.Enable();
                continue;
            }
            map.Disable();
        }
    }


    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        fpsInputs.cursorInputForLook = newState;
    }
    
    private void OnDestroy()
    {
        _gameManager.GameModeChanged -= OnGameModeChanged;
    }
}