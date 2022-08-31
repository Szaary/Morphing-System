using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSwitcher : AbstractGameModeSwitcherMono
{
    [SerializeField] private GameMode gameMode;
    
    protected override void OnGameModeChanged(GameMode newMode)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterFacade facade))
        {
            if (facade.isControlled)
            {
                GameManager.SetGameMode(gameMode);
            }
        }
            
    }
}
