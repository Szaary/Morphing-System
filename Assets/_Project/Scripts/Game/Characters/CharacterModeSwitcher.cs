using System;
using StarterAssets;
using UnityEngine;


public class CharacterModeSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject fpsLogic;
    [SerializeField] private GameObject logic2D;
    
    [SerializeField] private FirstPersonController fpsController;
   
    
    private CharacterFacade _facade;

    public void Initialize(CharacterFacade characterFacade)
    {
        _facade = characterFacade;
    }

    private void Start()
    {
        OnGameModeChanged(_facade.gameManager.GameMode);
        _facade.gameManager.GameModeChanged += OnGameModeChanged;
    }

    private void OnGameModeChanged(GameMode gameMode)
    {
        SetGameMode(gameMode);
    }

    private void OnDestroy()
    {
        _facade.gameManager.GameModeChanged -= OnGameModeChanged;
    }


    private void SetTurnBased(bool isEnabled)
    {
        logic2D.SetActive(isEnabled);
    }

    private void SetFps(bool isEnabled)
    {
        fpsLogic.SetActive(isEnabled);
    }

  
    private void SetGameMode(GameMode gameMode)
    {
        if (gameMode == GameMode.TurnBasedFight)
        {
            SetFps(false);
            SetTurnBased(true);
           
        }
        else if (gameMode == GameMode.Fps)
        {
            SetTurnBased(false);
            SetFps(true);
        }
    }


}