using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GameModeSwitcher : AbstractGameModeSwitcherMono
{
    [SerializeField] private BoxCollider boxCollider;
    

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

    
    private void OnDrawGizmos()
    {
        if (boxCollider == null) return;
        GizmosExtensions.DrawBoxCollider(Color.red, boxCollider, 0.2f, transform);
    }
 

}