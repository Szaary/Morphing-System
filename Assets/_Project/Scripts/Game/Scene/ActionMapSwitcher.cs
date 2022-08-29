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
    
    private InputActionMap _turnBased;
    private InputActionMap _fpsInput;
    private InputActionMap _universal;
    private InputActionMap _menu;

    private readonly List<InputActionMap> _actionMaps=new();
    
    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Start()
    {
        _turnBased= playerInput.actions.FindActionMap("TurnBasedInput");
        _fpsInput =  playerInput.actions.FindActionMap("FpsInput");
        _menu =  playerInput.actions.FindActionMap("UI");
        _universal = playerInput.actions.FindActionMap("Universal");
        
        _actionMaps.Add(_turnBased);
        _actionMaps.Add(_fpsInput);
        _actionMaps.Add(_menu);
        
        _universal.Enable();
        
        OnGameModeChanged(_gameManager.GameMode);
        _gameManager.GameModeChanged += OnGameModeChanged;
    }

    private void OnGameModeChanged(GameMode newMode)
    {
        if (newMode == GameMode.TurnBasedFight)
        {
            SelectActionMap(_turnBased);
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
