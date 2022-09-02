using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMapSwitcher : AbstractGameModeSwitcherMono
{
    [SerializeField] private PlayerInput playerInput;

    private GameManager _gameManager;

    [SerializeField] private MovementInput movementInputs;

    private InputActionMap _turnBased;
    private InputActionMap _movementInput;
    private InputActionMap _universal;
    private InputActionMap _menu;

    private readonly List<InputActionMap> _actionMaps = new();


    protected override void Start()
    {
        _turnBased = playerInput.actions.FindActionMap("TurnBasedInput");
        _movementInput = playerInput.actions.FindActionMap("Movement");
        _menu = playerInput.actions.FindActionMap("UI");
        _universal = playerInput.actions.FindActionMap("Universal");

        _actionMaps.Add(_turnBased);
        _actionMaps.Add(_movementInput);
        _actionMaps.Add(_menu);

        _universal.Enable();

        base.Start();
    }

    protected override void OnGameModeChanged(GameMode newMode)
    {
        if (newMode == GameMode.TurnBasedFight)
        {
            SelectActionMap(_turnBased);
            SetCursorState(false);
        }
        else if (newMode is GameMode.Fps or GameMode.Adventure or GameMode.Tpp)
        {
            SelectActionMap(_movementInput);
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
        movementInputs.cursorInputForLook = newState;
    }
}