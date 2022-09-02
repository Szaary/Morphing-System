using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModesDebugger : MonoBehaviour
{
    [SerializeField] private UniversalInput input;
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        if(input.onFps) gameManager.SetGameMode(GameMode.Fps);
        if(input.onTurn) gameManager.SetGameMode(GameMode.TurnBasedFight);
        if(input.onTpp) gameManager.SetGameMode(GameMode.Tpp);
    }
}
