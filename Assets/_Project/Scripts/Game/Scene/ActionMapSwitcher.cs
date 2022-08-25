using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class ActionMapSwitcher : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private StarterAssetsInputs fpsInputs; 
    
    
    private GameManager _gameManager;

    private InputActionMap _turnBasedInput;
    private InputActionMap _fpsInput;
    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Start()
    {
        _turnBasedInput= playerInput.actions.FindActionMap("TurnBasedInput");
        _fpsInput =  playerInput.actions.FindActionMap("FpsInput");
        
        OnGameModeChanged(_gameManager.GameMode);
        _gameManager.GameModeChanged += OnGameModeChanged;
    }

    private void OnGameModeChanged(GameMode newMode)
    {
        if (newMode == GameMode.TurnBasedFight)
        {
            _fpsInput.Disable();
            _turnBasedInput.Enable();
            SetCursorState(false);
        }
        else if (newMode== GameMode.Fps)
        {
            _turnBasedInput.Disable(); 
            _fpsInput.Enable();
            SetCursorState(true);
            
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
